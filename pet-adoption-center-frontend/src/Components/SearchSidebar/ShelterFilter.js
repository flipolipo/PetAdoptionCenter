import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { address_url } from '../../Service/url';

function ShelterFilter({ onChange }) {
  const [shelters, setShelters] = useState([]);
  const [selectedShelter, setSelectedShelter] = useState('');

  useEffect(() => {
    const fetchShelters = async () => {
      try {
        const response = await axios.get(`${address_url}/Shelters`);
        setShelters(response.data);
      } catch (error) {
        console.log(error.message);
      }
    };
    fetchShelters();
  }, []);

  const handleShelterChange = (event) => {
    const shelterId = event.target.value;
    setSelectedShelter(shelterId);
    onChange(shelterId);
  };

  return (
    <div className="filter">
      <h3>Filter by Shelter</h3>
      <select value={selectedShelter} onChange={handleShelterChange}>
        <option value="">Select a shelter</option>
        {shelters ? (
          shelters.map((shelter) => (
            <option key={shelter.Id} value={shelter.Id}>
              {`${shelter.Name} in ${shelter.ShelterAddress.City}`}
            </option>
          ))
        ) : null}
      </select>
    </div>
  );
}

export default ShelterFilter;