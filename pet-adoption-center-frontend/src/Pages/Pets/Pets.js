import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useUser } from '../../Components/UserContext';
import { address_url } from '../../Service/url';
import SearchSidebar from './SearchSidebar/SearchSidebar';
import FlipCardAvailable from '../../Components/FlipCardAvailable';
import FlipCardAll from '../../Components/FlipCardAll';
import './Pets.css';

const Pets = () => {
  const { user } = useUser();
  const [userPets, setUserPets] = useState(null);
  const [petData, setPetData] = useState(null);
  const [selectedShelter, setSelectedShelter] = useState('');
  const [error, setError] = useState(null);

  // useEffect(() => {
  //   const fetchUserPets = async () => {
  //     try {
  //       if (user && user.id) {
  //         const response = await axios.get(
  //           `${address_url}/Users/${user.id}/pets`,
  //           {
  //             headers: {
  //               Authorization: `Bearer ${user.token}`,
  //             },
  //           }
  //         );
  //         setUserPets(response.data);
  //       }
  //     } catch (error) {
  //       console.error('Error fetching user pets:', error);
  //     }
  //   };

  //   fetchUserPets();
  // }, [user.id, user.token]);

  useEffect(() => {
    const fetchPetData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/pets`, {
          headers: {
            'Authorization': `Bearer ${user.token}`
          }
        });
        setPetData(response.data);
      } catch (err) {
        setError(err.message);
      }
    };
    fetchPetData();
  }, [user.id, user.token]);

  const handleShelterChange = (shelterId) => {
    setSelectedShelter(shelterId);
  };

  return (
    <div className='pet-page-container'>
      <h2>Welcome, {user.username}!</h2>
       <div className='pet-filter'>
        <div className='find-your-pet-buttons'>
          <SearchSidebar
            handleFilterResults={(filteredPets) => {
              console.log(filteredPets);
            }}
          />
        </div>
      </div>
      <div className="petsAvailableForAdoption">
        <div className="pet-inscription">
          <h2>Pets available for adoption</h2>
        </div>
        <div className="pet-card">
          <FlipCardAvailable />
        </div>
      </div><div className="petsAll">
        <div className="pet-inscription">
          <h2>All Pets</h2>
        </div>
        <div className="pet-card">
          <FlipCardAll />
        </div>
      </div>
      
     
    </div>
  );
};

export default Pets;






