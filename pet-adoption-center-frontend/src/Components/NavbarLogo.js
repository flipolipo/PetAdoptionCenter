import React from 'react';
import { Link } from 'react-router-dom';
import logo from '../Photo/Round_full.png';
import './NavbarLogo.css'


export const NavbarLogo = () => {
  return (
    <div className="navbar2">
      <Link className="sign-in1">Sign In</Link>
      <Link className="sign-up1"> Sign Up</Link>
      <img className="round-full-icon1" alt="" src={logo} />
    </div>
  )
}
