import React from 'react';
import { address_url } from './url.js';
import axios from 'axios';

const FetchDataForPets = async () => {
  try {
    const response = await axios.get(`${address_url}/Users/pets`);
    return response.data;
  } catch (error) {
    console.log(error.message);
  }
};
export default FetchDataForPets;