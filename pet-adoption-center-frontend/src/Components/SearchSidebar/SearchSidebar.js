import React, { useState, useEffect } from "react";
import { address_url } from '../../Service/url';
import axios from 'axios';
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
  const [selectedShelter, setSelectedShelter] = useState('');
  const [selectedGender, setSelectedGender] = useState(-1);
  const [selectedSize, setSelectedSize] = useState(-1);
  const [selectedType, setSelectedType] = useState('');
  const [selectedStatus, setSelectedStatus] = useState(-1);
  const [petsFiltered, setPetsFiltered] = useState([]);
  const [showNoMatchingPets, setShowNoMatchingPets] = useState(false);
  const [showPetsList, setShowPetsList] = useState(true);



  useEffect(() => {
    const fetchShelters = async () => {
      try {
        const response = await axios.get(`${address_url}/Shelters`);
        setShelters(response.data);
      } catch (error) {
        console.log(error.message);
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
        console.log(error.message);
      }
    };
    fetchPetData();
  }, []);

  const handleShelterChange = (shelterId) => {
    setSelectedShelter(shelterId);
  };

  const handleFilter = () => {
  
    const filteredPets = petsData.filter((pet) => {
      if (selectedShelter) {
        if (selectedGender === 0) {
          return (
            pet.ShelterId === selectedShelter &&
            pet.Gender === 0 &&
            (selectedSize === -1 || pet.BasicHealthInfo.Size === selectedSize) &&
            (selectedType === '' || pet.Type === selectedType) &&
            (selectedStatus === -1 || pet.Status === selectedStatus)
          );
        } else if (selectedGender === 1) {
          return (
            pet.ShelterId === selectedShelter &&
            pet.Gender === 1 &&
            (selectedSize === -1 || pet.BasicHealthInfo.Size === selectedSize) &&
            (selectedType === '' || pet.Type === selectedType) &&
            (selectedStatus === -1 || pet.Status === selectedStatus)
          );
        } else {
          return (
            pet.ShelterId === selectedShelter &&
            (selectedSize === -1 || pet.BasicHealthInfo.Size === selectedSize) &&
            (selectedType === '' || pet.Type === selectedType) &&
            (selectedStatus === -1 || pet.Status === selectedStatus)
          );
        }
      } else {
        if (selectedGender === 0) {
          return (
            pet.Gender === 0 &&
            (selectedSize === -1 || pet.BasicHealthInfo.Size === selectedSize) &&
            (selectedType === '' || pet.Type === selectedType) &&
            (selectedStatus === -1 || pet.Status === selectedStatus)
          );
        } else if (selectedGender === 1) {
          return (
            pet.Gender === 1 &&
            (selectedSize === -1 || pet.BasicHealthInfo.Size === selectedSize) &&
            (selectedType === '' || pet.Type === selectedType) &&
            (selectedStatus === -1 || pet.Status === selectedStatus)
          );
        } else {
          return (
            (selectedSize === -1 || pet.BasicHealthInfo.Size === selectedSize) &&
            (selectedType === '' || pet.Type === selectedType) &&
            (selectedStatus === -1 || pet.Status === selectedStatus)
          );
        }
      }
    });
  
    setPetsFiltered(filteredPets);
    setShowNoMatchingPets(filteredPets.length === 0);
    setShowPetsList(filteredPets.length > 0);
  };

  const handleBack = () => {
    setShowNoMatchingPets(false);
    setPetsFiltered([]);
  };


    return (
      <div className="filtered-pets">
        <div className="sidebar">
          <ShelterFilter shelters={shelters} setShelters={setShelters} onChange={handleShelterChange} />
          <GenderFilter onChange={setSelectedGender} />
          <SizeFilter onChange={setSelectedSize} />
          <TypeFilter onChange={setSelectedType} />
          <StatusFilter onChange={setSelectedStatus} />
          <button className="filter-button" onClick={handleFilter}>Apply Filters</button>
        </div>
        <div className="card-container">
        {showNoMatchingPets && (
          <div className="no-matching">
            <div className="no-matching-pets">
              <p>No pets match the selected filters.</p>
            </div>
            <div className="back">
            <button className="back-button" onClick={handleBack}>back</button>
            </div>
          </div>
        )}
        {!showNoMatchingPets && petsFiltered.length > 0 && (
          petsFiltered.map((pet, index) => (
            <GenericCard key={index} pet={pet} />
          ))
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