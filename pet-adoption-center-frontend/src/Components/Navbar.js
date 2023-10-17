import React, { useState } from 'react';
import { Link, NavLink } from 'react-router-dom';
import './Navbar.css';

const Navbar = () => {
  const [menuOpen, setMenuOpen] = useState(false);

  const handleClick = () => setMenuOpen(!menuOpen);

  return (
    <>
      <nav className="navbar">
        <div className="navbar-container">
          <img
            className="navbar-logo"
            alt=""
            src={process.env.PUBLIC_URL + '/Photo/Round_full.png'}
            style={{ width: '120px', height: 'auto' }}
          />
          <span></span>
          <span></span>
          <span></span>
          <NavLink to="/sign-in" className="nav-links-logo">
            Sign In
          </NavLink>
          <NavLink to="/sign-up" className="nav-links-logo">
            Sign Up
          </NavLink>
        </div>
        <div className="menu" onClick={handleClick}>
          <span></span>
          <span></span>
          <span></span>
        </div>
        <div className="menu-icon" onClick={handleClick}>
          <ul className={menuOpen ? 'open' : ''}>
            <li className="nav-item-navigation">
              <Link to="/" className="nav-links">
                HOME
              </Link>
            </li>
            <li className="nav-item-navigation">
              <NavLink to="/Shelters/Adoptions" className="nav-links">
                ADOPTION
              </NavLink>
            </li>
            <li className="nav-item-navigation">
              <NavLink to="/Shelters" className="nav-links">
                SHELTERS
              </NavLink>
            </li>
            <li className="nav-item-navigation">
              <NavLink to="/Shelters/TemporaryHouses" className="nav-links">
                TEMPORARY HOUSE
              </NavLink>
            </li>
            <li className="nav-item-navigation">
              <NavLink to="/Users/pets" className="nav-links">
                PETS
              </NavLink>
            </li>
          </ul>
        </div>
      </nav>
    </>
  );
};

export default Navbar;
