import React from 'react';
import './Home.css';
import { Link } from 'react-router-dom';
import FlipCard from '../../Components/FlipCard';

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
        <h2>Pets available for adoption</h2>
        <FlipCard />
      </div>
    </div>
  );
};
export default Home;
