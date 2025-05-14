import {address_url} from './url.js';
import axios from "axios";

export const fetchDataForPet = async (petId) => {
    try {
        const response = await axios.get(`${address_url}/Users/pets/${petId}`);
        return (response.data);
      } catch (error) {
        console.error(error);
      }
}
