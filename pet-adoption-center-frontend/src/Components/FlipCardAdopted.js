import React, { useState, useEffect } from 'react';
import './FlipCard.css';
import axios from 'axios';
import GenericCard from './GenericCard';
import { address_url } from '../Service/url.js';

const FlipCardAdopted = () => {
  const [petsAdopted, setPetsAdopted] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [petsPerPage] = useState(3);

  useEffect(() => {
    GetAdoptedPets();
    console.log(`Adopted Pet ${currentPets}`)
  }, []);

  async function GetAdoptedPets() {
    try {
      const response = await axios.get(`${address_url}/Users/pets/adopted`);
      console.log(response.data);
      setPetsAdopted(response.data);
    } catch (error) {
      console.error(error);
    }
  }

  const indexOfLastPet = currentPage * petsPerPage;
  const indexOfFirstPet = indexOfLastPet - petsPerPage;
  const currentPets = petsAdopted.slice(indexOfFirstPet, indexOfLastPet);
  
  return (
    <div>
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
          disabled={indexOfLastPet >= petsAdopted.length}
          className="button-pagination"
        >
          Next
        </button>
      </div>
    </div>
  );
};

export default FlipCardAdopted;
