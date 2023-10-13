import axios from 'axios';
import { address_url } from './url';

export const fetchData = async (endpoint) => {
  try {
    const response = await axios.get(`${address_url}/${endpoint}`);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};
