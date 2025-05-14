import React, { useState, useEffect } from "react";
import { address_url } from '../../Service/url';
import axios from "axios";
import ShelterFilter from "./ShelterFilter";
import TypeFilter from "./TypeFilter";
import GenderFilter from "./GenderFilter";
import SizeFilter from "./SizeFilter";
import StatusFilter from "./StatusFilter";
import GenericCard from "../GenericCard";
import FlipCardAvailable from "../FlipCardAvailable";

function SearchSidebar() {
  const [shelters, setShelters] = useState([]);
  const [petsData, setPetsData] = useState(null);
  const [selectedShelter, setSelectedShelter] = useState("");
  const [selectedGender, setSelectedGender] = useState("");
  const [selectedSize, setSelectedSize] = useState("");
  const [selectedType, setSelectedType] = useState("");
  const [selectedStatus, setSelectedStatus] = useState("");
  const [petsFiltered, setPetsFiltered] = useState([]);
  const [showNoMatchingPets, setShowNoMatchingPets] = useState(false);
  const [,setShowPetsList] = useState(true);

  useEffect(() => {
    const fetchShelters = async () => {
      try {
        const response = await axios.get(`${address_url}/Shelters`);
        setShelters(response.data);
      } catch (error) {
        console.error("Error fetching shelters:", error.message);
      }
    };
    fetchShelters();
  }, []);

  useEffect(() => {
    const fetchPetData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/pets`);
        setPetsData(response.data);
      } catch (error) {
        console.error("Error fetching pet data:", error.message);
      }
    };
    fetchPetData();
  }, []);

  const handleShelterChange = (shelterId) => {
    setSelectedShelter(shelterId);
  };

  const applyFilters = () => {
    const filteredPets = petsData.filter((pet) => {
      const shelterFilter = !selectedShelter || pet.ShelterId === selectedShelter;
      const genderFilter = selectedGender === "" || pet.Gender === selectedGender;
      const sizeFilter = selectedSize === "" || (pet.BasicHealthInfo && pet.BasicHealthInfo.Size === selectedSize);
      const typeFilter = selectedType === "" || pet.Type === selectedType;
      const statusFilter = selectedStatus === "" || pet.Status === selectedStatus;

      return shelterFilter && genderFilter && sizeFilter && typeFilter && statusFilter;
    });

    setPetsFiltered(filteredPets);
    setShowNoMatchingPets(filteredPets.length === 0);
    setShowPetsList(filteredPets.length > 0);
  };

  const handleBack = () => {
    console.log("Clearing filters and going back");
    setShowNoMatchingPets(false);
    setPetsFiltered([]);
    setSelectedShelter("");
    setSelectedGender("");
    setSelectedSize("");
    setSelectedType("");
    setSelectedStatus("");
  };

  return (
    <div className="filtered-pets">
      <div className="sidebar">
        <ShelterFilter shelters={shelters} onChange={handleShelterChange} value={selectedShelter} />
        <GenderFilter onChange={setSelectedGender} value={selectedGender} />
        <SizeFilter onChange={setSelectedSize} value={selectedSize} />
        <TypeFilter onChange={setSelectedType} value={selectedType} />
        <StatusFilter onChange={setSelectedStatus} value={selectedStatus} />
        <button className="filter-button" onClick={applyFilters}>
          Apply Filters
        </button>
      </div>
      <div className="card-container">
        {showNoMatchingPets && (
          <div className="no-matching">
            <div className="no-matching-pets">
              <p>No pets match the selected filters.</p>
            </div>
            <div className="back">
              <button className="back-button" onClick={handleBack}>
                Back
              </button>
            </div>
          </div>
        )}
        {!showNoMatchingPets && petsFiltered.length > 0 && (
          petsFiltered.map((pet, index) => <GenericCard key={index} pet={pet} />)
        )}
        {!showNoMatchingPets && petsFiltered.length === 0 && (
          <div className="petsAvailableForAdoption">
            <div className="pet-inscription">
              <h2>Pets available for adoption</h2>
            </div>
            <div className="pet-card">
              <FlipCardAvailable />
            </div>
          </div>
        )}
      </div>
    </div>
  );
}

export default SearchSidebar;

