import axios from 'axios';
import { address_url } from './url';

export const deleteData = async (endpoint, id) => {
  try {
    const response = await axios.delete(`${address_url}/${endpoint}/${id}`);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};
