import React from 'react';
import './Home.css';
import { Link } from 'react-router-dom';
import FlipCardAvailable from '../../Components/FlipCardAvailable';

const Home = () => {
  return (
    <div className="homepage-container">
      <div className="pets">
        <img
          className="image-9-icon"
          alt=""
          src={process.env.PUBLIC_URL + '/Photo/pets.png'}
        />
        <div className="rectanglePets">
          <Link to="/Users/pets" className="find-your-new">
            Find your new best friend
          </Link>
        </div>
      </div>
      <div className="petsAvailableForAdoption">
        <div className="pet-inscription">
          <h2>Pets available for adoption</h2>
        </div>
        <div className="pet-card">
          <FlipCardAvailable />
        </div>
      </div>
    </div>
  );
};
export default Home;
