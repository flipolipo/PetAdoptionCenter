import { address_url } from './url';
import axios from 'axios';

export const addData = async (endpoint, newPost) => {
  try {
    const response = await axios.post(`${address_url}/${endpoint}`, newPost);
    return response.data;
  } catch (error) {
    console.log(error);
  }
};
