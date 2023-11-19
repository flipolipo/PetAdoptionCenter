import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Select from 'react-select';
import { address_url } from '../../Service/url';

function ShelterFilter({ onChange }) {
  const [shelters, setShelters] = useState([]);
  const [filteredShelters, setFilteredShelters] = useState([]);
  const [selectedShelter, setSelectedShelter] = useState(null);
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    const fetchShelters = async () => {
      try {
        const response = await axios.get(`${address_url}/Shelters`);
        setShelters(response.data);
        setFilteredShelters(response.data);
      } catch (error) {
        console.log(error.message);
      }
    };
    fetchShelters();
  }, []);

  const handleInputChange = (term) => {
    setSearchTerm(term);

    const filtered = shelters.filter((shelter) => {
      const shelterName = `${shelter.Name} in ${shelter.ShelterAddress.City}`;
      return shelterName.toLowerCase().includes(term.toLowerCase());
    });

    setFilteredShelters(filtered);
  };

  const handleSelectChange = (selectedOption) => {
    setSelectedShelter(selectedOption);
    onChange(selectedOption?.value || '');
  };

  const shelterOptions = filteredShelters.map((shelter) => ({
    value: shelter.Id,
    label: `${shelter.Name} in ${shelter.ShelterAddress.City}`
  }));

  return (
    <div className="filter">
      <h3>Filter by Shelter</h3>
      <div className='select-container'>
        <Select
          value={selectedShelter}
          onChange={handleSelectChange}
          options={shelterOptions}
          placeholder="Type city / shelter name"
          isSearchable
          onInputChange={handleInputChange}
          className="select-shelter"
        />
      </div>
    </div>
  );
}

export default ShelterFilter;