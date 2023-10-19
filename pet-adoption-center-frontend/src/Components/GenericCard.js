import React from 'react';
import { Link } from 'react-router-dom';
import GenderPetLabel from './Enum/GenderPetLabel';
import SizePetLabel from './Enum/SizePetLabel';
import './FlipCard.css';

const GenericCard = ({ pet }) => {
  return (
    <div className="card">
      <div className="flip-card">
        <div className="flip-card-inner">
          <div className="flip-card-front">
            <div className="image-container">
              <img
                src={`data:image/jpeg;base64, ${pet.ImageBase64}`}
                alt=""
                width="250px"
                height="100%"
              />
              <img
                src={process.env.PUBLIC_URL + '/Photo/whitePaw.png'}
                alt="Lapka"
                className="paw-icon"
              />
            </div>
            <center className='pet-name'>
              <h3>{pet.BasicHealthInfo.Name}</h3>
            </center>
          </div>
          <div className="flip-card-back">
            <center>
              <h2>Age: {pet.BasicHealthInfo.Age}</h2>
              <h2>Size: {SizePetLabel(pet.BasicHealthInfo.Size)}</h2>
              <h2>Gender: {GenderPetLabel(pet.Gender)}</h2>
              <h2>
                {' '}
                Description: {' '}
                {pet.Description.length > 30
                  ? pet.Description.substring(0, 30) + '...'
                  : pet.Description}
              </h2>
              <div className="more-inf">
                <Link to={`/Users/pets/${pet.Id}`} className="inf-link">
                  More information
                </Link>
              </div>
            </center>
          </div>
        </div>
      </div>
    </div>
  );
};

export default GenericCard;
