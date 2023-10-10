import React from 'react';
import logo from '../Photo/Round_full.png';
import './NavbarLogo.css'


export const NavbarLogo = () => {
  return (
    <div className="navbar2">
      <a className="sign-in1">Sign In</a>
      <a className="sign-up1"> Sign Up</a>
      <img className="round-full-icon1" alt="" src={logo} />
    </div>
  )
}
