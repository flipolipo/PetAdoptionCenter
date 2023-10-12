import React from 'react';
import redPaw from '../Photo/redPaw.png';
import whitePaw from '../Photo/whitePaw.png';
import pets from '../Photo/pets.png';
import './Home.css';
import { Link } from 'react-router-dom';

const Home = () => {
  return (
    <div className="homepage-container">
    <div className="pets">
        <img className="image-9-icon" alt="" src={pets}/>
        <Link to="/Users/pets" className="find-your-new">Find your new best friend</Link>
    </div>
  </div>
  )
}
export default Home