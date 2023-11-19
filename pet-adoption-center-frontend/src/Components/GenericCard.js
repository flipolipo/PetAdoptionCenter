import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import GenderPetLabel from './Enum/GenderPetLabel';
import SizePetLabel from './Enum/SizePetLabel';
import { useState } from 'react';
import axios from 'axios';
import { useUser } from './UserContext';
import { address_url } from '../Service/url';
//import './FlipCard.css';

const GenericCard = ({ pet }) => {
  const [isHovered, setIsHovered] = useState(false);
  const { user, setUser } = useUser();
  const [favoured, setFavoured] = useState(false);
  const [userData, setUserData] = useState({});
  const handleMouseEnter = () => {
    setIsHovered(true);
    console.log('entered');
  };
  const GetUser = async () => {
    try {
      const response = await axios.get(`${address_url}/Users/${user.id}`);
      console.log(response.data);
      setUserData(response.data);
    } catch (error) {
      console.log(error);
    }
  };
  useEffect(() => {
    const getUser = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${user.id}`);
        setUserData(response.data);
      } catch (error) {
        console.log(error);
      }
    };

    getUser();
  }, [user]);
  const CheckForFavourite = () => {
    return (
      userData &&
      userData.Pets &&
      userData.Pets.some((userPet) => userPet.Id === pet.Id)
    );
  };
  useEffect(() => {
    setFavoured(CheckForFavourite(userData));
  }, [userData]);
  console.log(user);
  const handleMouseLeave = () => {
    setIsHovered(false);
  };
  useEffect(() => {
    getImageSrc();
  }, [favoured]);
  const getImageSrc = () => {
    if (user.isLogged) {
      return favoured
        ? process.env.PUBLIC_URL + '/Photo/redPaw.png'
        : isHovered
        ? process.env.PUBLIC_URL + '/Photo/redPaw.png'
        : process.env.PUBLIC_URL + '/Photo/whitePaw.png';
    } else {
      return isHovered
        ? process.env.PUBLIC_URL + '/Photo/redPaw.png'
        : process.env.PUBLIC_URL + '/Photo/whitePaw.png';
    }
  };
  console.log(pet);
  const ClickHandler = async () => {
    if (user.isLogged) {
      try {
        if (favoured) {
          setFavoured(false);
          await axios.delete(
            `${address_url}/Users/${user.id}/pets/${pet.Id}`
          );
        } else {
          setFavoured(true);
          await axios.post(
            `${address_url}/Users/${user.id}/pets/${pet.Id}`
          );
        }
      } catch (error) {
        console.log(error);
      }
    } else {
      console.log('You have to log in!');
    }
  };
  return (
    <div className="card">
      <div className="flip-card">
        <div className="paw-container">
          <img
            onClick={ClickHandler}
            onMouseEnter={handleMouseEnter}
            onMouseLeave={handleMouseLeave}
            src={getImageSrc()}
            alt="Lapka"
            className="paw-icon"
          />
        </div>
        <div className="flip-card-inner">
          <div className="flip-card-front">
            <div className="image-container">
              <img
                src={`data:image/jpeg;base64, ${pet.ImageBase64}`}
                alt=""
                width="250px"
                height="100%"
              />
            </div>
            <center className="pet-name">
              <h3>{pet.BasicHealthInfo.Name}</h3>
            </center>
          </div>
          <div className="flip-card-back">
            <center>
              <h2>Age: {pet.BasicHealthInfo.Age}</h2>
              <h2>Size: {SizePetLabel(pet.BasicHealthInfo.Size)}</h2>
              <h2>Gender: {GenderPetLabel(pet.Gender)}</h2>
              <h2>
                {' '}
                Description:{' '}
                {pet.Description.length > 30
                  ? pet.Description.substring(0, 30) + '...'
                  : pet.Description}
              </h2>
              <div className="more-inf">
                <Link to={`/Users/pets/${pet.Id}`} className="inf-link">
                  More information
                </Link>
              </div>
            </center>
          </div>
        </div>
      </div>
    </div>
  );
};

export default GenericCard;
