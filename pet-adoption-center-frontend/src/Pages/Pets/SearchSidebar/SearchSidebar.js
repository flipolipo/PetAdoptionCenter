import React, { useState, useEffect } from "react";
import { useUser } from '../../../Components/UserContext';
import { address_url } from '../../../Service/url';
import axios from 'axios';
import ShelterFilter from "./ShelterFilter";
import TypeFilter from "./TypeFilter";
import GenderFilter from "./GenderFilter";
import SizeFilter from "./SizeFilter";
import GenericCard from "../../../Components/GenericCard";

function SearchSidebar({ handleFilterResults }) {
  const { user } = useUser();
  const [shelters, setShelters] = useState([]);
  const [selectedShelter, setSelectedShelter] = useState('');
  const [selectedGender, setSelectedGender] = useState('');
  const [selectedSize, setSelectedSize] = useState('');
  const [selectedType, setSelectedType] = useState('');
  const [petsFiltered, setPetsFiltered] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [petsPerPage] = useState(4);

  useEffect(() => {
    const fetchShelters = async () => {
      try {
        const response = await axios.get(`${address_url}/Shelters`, {
          headers: {
            Authorization: `Bearer ${user.token}`,
          },
        });
        setShelters(response.data);
      } catch (err) {
        console.error(err.message);
      }
    };
    fetchShelters();
  }, [user.token]);

  const handleShelterChange = (shelterId) => {
    setSelectedShelter(shelterId);
  };

  const handleFilter = async () => {
    // if (user && user.token) {
      const selectedShelterId = selectedShelter;

      const response = await axios.get(`${address_url}/Users/pets/filtered`, {
        // headers: {
        //   Authorization: `Bearer ${user.token}`,
        // },
        params: {
          shelter: selectedShelterId,
          gender: selectedGender,
          size: selectedSize,
          type: selectedType,
        },
      });

      handleFilterResults(response.data);
      setPetsFiltered(response.data);
    // } else {
    //   console.error('User or user.token is undefined');
    // }
  };

  const indexOfLastPet = currentPage * petsPerPage;
  const indexOfFirstPet = indexOfLastPet - petsPerPage;
  const currentPets = petsFiltered.slice(indexOfFirstPet, indexOfLastPet);

  return (
      <div className="filtered-pets">
        <div className="sidebar">
        <ShelterFilter
          shelters={shelters}
          setShelters={setShelters}
          onChange={handleShelterChange}
        />
        <TypeFilter onChange={setSelectedType} />
        <GenderFilter onChange={setSelectedGender} />
        <SizeFilter onChange={setSelectedSize} />
        <button onClick={handleFilter}>Apply Filters</button>
      </div>
      <div className="card-container">
        {currentPets.map((pet, index) => (
          <GenericCard key={index} pet={pet} />
        ))}
      </div>
      <div className="pagination">
        <button
          onClick={() => setCurrentPage(currentPage - 1)}
          disabled={currentPage === 1}
          className="button-pagination"
        >
          Previous
        </button>
        <button
          onClick={() => setCurrentPage(currentPage + 1)}
          disabled={indexOfLastPet >= petsFiltered.length}
          className="button-pagination"
        >
          Next
        </button>
      </div>
      </div>
  );
}

export default SearchSidebar;

