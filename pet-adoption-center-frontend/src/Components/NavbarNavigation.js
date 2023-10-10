import React from 'react';
import { Link } from 'react-router-dom';
import paw from '../Photo/paw.svg';
import './NavbarNavigation.css';

export const NavbarNavigation = () => {
  return (
    <div className="navbar11">
    <div className="navbar1-item" />
    <Link to='/' className="home1">HOME</Link>
    <Link to='/Shelters/Adoptions' className="adoption1">ADOPTION</Link>
    <img className="vector-icon26" alt="" src={paw} />
    <Link to='/Shelters' className="shelters1">SHELTERS</Link>
    <img className="vector-icon27" alt="" src={paw} />
    <Link to='/Shelters/TemporaryHouses' className="temporary-house1">TEMPORARY HOUSE</Link>
    <Link to='/Users/pets' className="pets3">PETS</Link>
  </div>
  )
}
