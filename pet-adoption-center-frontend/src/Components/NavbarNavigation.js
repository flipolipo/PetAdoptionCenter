import React from 'react';
import paw from '../Photo/paw.svg'

export const NavbarNavigation = () => {
  return (
    <div className="navbar11">
    <div className="navbar1-item" />
    <a className="home1">HOME</a>
    <a className="adoption1">ADOPTION</a>
    <img className="vector-icon26" alt="" src={paw} />
    <a className="shelters1">SHELTERS</a>
    <img className="vector-icon27" alt="" src={paw} />
    <a className="temporary-house1">TEMPORARY HOUSE</a>
    <a className="pets3">PETS</a>
  </div>
  )
}
