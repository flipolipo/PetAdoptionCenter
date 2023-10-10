import React, { useState } from 'react';
import email from '../Photo/email.png'
import fb from '../Photo/Facebook.png';
import instagram from '../Photo/instagram.png'
import paw from '../Photo/paw.svg';
import redPaw from '../Photo/redPaw.png';
import whitePaw from '../Photo/whitePaw.png';
import pets from '../Photo/pets.png';
import phone from '../Photo/phone.png';

export const Home = () => {
  return (
    <div className="homepage">
  
    <div className="pets2">
      <img className="vector-icon8" alt="" src={paw} />
      <img className="vector-icon9" alt="" src={paw} />
      <img className="vector-icon10" alt="" src={paw} />
      <img className="vector-icon11" alt="" src={paw} />
      <img className="vector-icon12" alt="" src={paw} />
      <img className="pets-item" alt="" src="/rectangle-41.svg" />
      <img className="image-9-icon1" alt="" src={pets} />
      <b className="find-your-new1">Find your new best friend</b>
    </div>
    <div className="petavailable">
      <div className="petcard31">
        <button className="buttonaddtofavourite">
          <div className="buttonaddtofavourite-child" />
          <img className="vector-icon13" alt="" src={redPaw} />
        </button>
        <img className="image-2-icon1" alt="" src="/image-2@2x.png" />
        <div className="petcard3-item" />
        <b className="bunia1">Bunia</b>
      </div>
      <div className="petcard11">
        <button className="buttonaddtofavourite1">
          <div className="buttonaddtofavourite-item" />
          <img className="vector-icon13" alt="" src={whitePaw} />
        </button>
        <div className="petcard1-item" />
        <b className="luna1">Luna</b>
        <img className="image-4-icon1" alt="" src="/image-4@2x.png" />
      </div>
      <div className="petcard41">
        <div className="petcard3-item" />
        <b className="bunia1">Kicia</b>
        <img className="image-6-icon1" alt="" src="/image-6@2x.png" />
        <button className="buttonaddtofavourite2">
          <div className="buttonaddtofavourite-item" />
          <img className="vector-icon13" alt="" src={whitePaw} />
        </button>
      </div>
      <div className="petcard21">
        <button className="buttonaddtofavourite1">
          <div className="buttonaddtofavourite-item" />
          <img className="vector-icon13" alt="" src={whitePaw} />
        </button>
        <div className="petcard1-item" />
        <b className="luna1">Fuffi</b>
        <img className="image-8-icon1" alt="" src="/image-8@2x.png" />
      </div>
      <div className="pettoadoptionwitharrow1">
        <b className="pets-available-to1">{`Pets Available to Adoption `}</b>
        <img className="vector-icon17" alt="" src="/vector12.svg" />
      </div>
    </div>
    <div className="linksframe">
      <img className="linksframe-child" alt="" src="/rectangle-17.svg" />
      <div className="links1">
        <b className="small-palls1">SMALL PALLS</b>
        <b className="contact2">CONTACT</b>
        <div className="small-links2">
          <b className="links2">LINKS</b>
          <a className="email2">Email</a>
          <a className="telefono2">Telefono</a>
          <a className="instagram2">Instagram</a>
          <a className="facebook2">Facebook</a>
        </div>
        <b className="join-our-newsletter1">Join our newsletter</b>
        <div className="submit-email1">
          <div className="submit-email-item" />
          <button className="button3" />
          <b className="submit1">SUBMIT</b>
          <input
            className="email-address1"
            placeholder="Email address"
            type="text"
          />
        </div>
        <a className="icon-facebook-v1-icon">
          <img className="c-icon" alt="" src={fb} />
          <img className="vector-icon19" alt="" src="/vector14.svg" />
        </a>
        <a className="icon-instagram">
          <img className="c-icon" alt="" src={instagram} />
          <img className="c-icon" alt="" src="/c1.svg" />
          <img className="c-icon" alt="" src="/c2.svg" />
          <img className="group-icon" alt="" src="/group.svg" />
        </a>
        <a className="icon-email">
          <img className="c-icon" alt="" src={email} />
          <img className="vector-icon21" alt="" src="/vector16.svg" />
          <img className="vector-icon22" alt="" src="/vector17.svg" />
        </a>
        <a className="icon-phone">
          <img className="c-icon" alt="" src={phone} />
          <img className="vector-icon24" alt="" src="/vector19.svg" />
          <img className="vector-icon25" alt="" src="/vector20.svg" />
        </a>
        <div className="links-child" />
      </div>
    </div>
   
  </div>
  )
}
