import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './FlipCard.css';
import GenderPetLabel from './Enum/GenderLabel';
import SizePetLabel from './Enum/SizeLabel';
import { Link } from 'react-router-dom';

const FlipCard = () => {
  const [petsAvailable, setPetsAvailable] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [petsPerPage] = useState(4);

  useEffect(() => {
    GetAvailablePetsForAdoption();
  }, []);

  async function GetAvailablePetsForAdoption() {
    try {
      const response = await axios.get(
        'https://localhost:7292/Users/pets/available-to-adoption'
      );
      console.log(response.data);
      setPetsAvailable(response.data);
    } catch (error) {
      console.error(error);
    }
  }
  const indexOfLastPet = currentPage * petsPerPage;
  const indexOfFirstPet = indexOfLastPet - petsPerPage;
  const currentPets = petsAvailable.slice(indexOfFirstPet, indexOfLastPet);

  return (
    <div>
      <div className="card-container">
        {currentPets.map((pet, index) => (
          <div className="card" key={index}>
            <div className="flip-card">
              <div className="flip-card-inner">
                <div className="flip-card-front">
                  <div className="image-container">
                    <img
                      src={`data:image/jpeg;base64, ${pet.ImageBase64}`}
                      alt="" width='250px' height='100%'
                    />
                    <img
                      src={process.env.PUBLIC_URL + '/Photo/whitePaw.png'}
                      alt="Lapka"
                      className="paw-icon"
                    />
                  </div>
                  <center>
                    <h3>{pet.BasicHealthInfo.Name}</h3>
                  </center>
                </div>
                <div className="flip-card-back">
                  <center>
                    <h2>Age: {pet.BasicHealthInfo.Age}</h2>
                    <h2>Size: {SizePetLabel(pet.BasicHealthInfo.Size)}</h2>
                    <h2>Gender: {GenderPetLabel(pet.Gender)}</h2>
                    <h2>Description: {pet.Description}</h2>
                    <Link to={`/Users/pets/${pet.Id}`}>More information</Link>
                  </center>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>
      <div className="pagination">
        <button
          onClick={() => setCurrentPage(currentPage - 1)}
          disabled={currentPage === 1}
        >
          Previous
        </button>
        <button
          onClick={() => setCurrentPage(currentPage + 1)}
          disabled={indexOfLastPet >= petsAvailable.length}
        >
          Next
        </button>
      </div>
    </div>
  );
};
export default FlipCard;
