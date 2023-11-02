import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { address_url } from '../../Service/url';
import SearchSidebar from './SearchSidebar/SearchSidebar';
import './Pets.css';

const Pets = () => {
  const [ setPetData ] = useState(null);

  useEffect(() => {
    const fetchPetData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/pets`);
        setPetData(response.data);
      } catch (error) {
        console.log(error.message);
      }
    };
    fetchPetData();
  }, []);

  return (
    <div className='pet-page-container'>
       <div className='pet-filter'>
        <div className='find-your-pet-buttons'>
          <SearchSidebar
            handleFilterResults={(filteredPets) => {
              console.log(filteredPets);
            }}
          />
        </div>
      </div>
    </div>
  );
};

export default Pets;