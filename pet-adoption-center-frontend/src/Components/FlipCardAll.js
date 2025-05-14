import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './FlipCard.css';
import GenericCard from './GenericCard';
import { address_url } from '../Service/url';

const FlipCardAll = () => {
  const [petsAll, setAllPets] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [petsPerPage] = useState(3);

  useEffect(() => {
    GetAllPets();
  }, []);

  async function GetAllPets() {
    try {
      const response = await axios.get(
        `${address_url}/Users/pets`
      );
      setAllPets(response.data);
    } catch (error) {
      console.error(error);
    }
  }

  const indexOfLastPet = currentPage * petsPerPage;
  const indexOfFirstPet = indexOfLastPet - petsPerPage;
  const currentPets = petsAll.slice(indexOfFirstPet, indexOfLastPet);

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
          disabled={indexOfLastPet >= petsAll.length}
          className="button-pagination"
        >
          Next
        </button>
      </div>
    </div>
  );
};

export default FlipCardAll;
