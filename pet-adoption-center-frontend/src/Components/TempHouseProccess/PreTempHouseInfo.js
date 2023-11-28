import React from 'react';
import './PreTempHouseInfo.css';
import { useUser } from '../UserContext';
import Login from '../Login';
import { Link } from 'react-router-dom';

const PreTempHouseInfo = () => {
  const { user } = useUser();
  return (
    <div className="preadoption-poll-info">
      {' '}
      <div className="preadoption-login-find-your-pet">
        {!user.isLogged && (
          <>
            {' '}
            <h2 className="preadoption-poll-info-h">Please</h2>
            <Login className="LoginComponent" />
            <h2 className="preadoption-poll-info-h">and </h2>
          </>
        )}
        <Link className="find-pet-link" to={`/Users/pets`}>
          FIND your new best friend
        </Link>{' '}
        <h2 className="preadoption-poll-info-h">first</h2>
      </div>
      <h2 className="preadoption-poll-info-h">PRE-TEMPORARY-HOUSE POLL</h2>
      <ul className="preadoption-poll-info-ul">
        <li>Name: *</li>
        <li>Surname: *</li>
        <li>City: *</li>
        <li>Street: *</li>
        <li>House number: *</li>
        <li>Flat number: *</li>
        <li>Postal code: *</li>
        <li>Are you 18 years or over?*</li>
        <li>
          Are you willing to make the investment in both time and finances to
          properly care for and manage your new pet?*
        </li>
        <li>Is there a fenced backyard?*</li>
        <li>How many adults are in your household?*</li>
        <li>Do ALL the members of your household want a new pet?*</li>
        <li>Have you relinquished or given away any pets before?*</li>
        <li>
          Please list current pets residing at your home (including roommates'
          pets also). Include pets' breed, age, sex, spay/neuter status, number
          of years owned, and if they live indoors or outdoors. *
        </li>
        <li>
          In a 24-hour day, how long would the pet be left alone at a given
          time?*
        </li>
        <li>Where will your new pet be kept when you are home?*</li>
        <li>Where will your new pet be kept when you are NOT at home?*</li>
        <li>In a 24-hour day, how long would the animal be indoors?*</li>
        <li>Do you already have a veterinarian? If yes, Name:*</li>
        <li>
          What arrangements would you make for your new animal if you went on a
          trip/vacation?*
        </li>
      </ul>
    </div>
  );
};

export default PreTempHouseInfo;
