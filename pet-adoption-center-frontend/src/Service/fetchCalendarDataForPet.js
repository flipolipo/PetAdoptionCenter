import axios from "axios";
import {address_url} from './url.js'

export const fetchCalendarDataForPet = async (petId) => {
    try {
        const response = await axios.get(`${address_url}/Users/pets/${petId}`);
        return (response.data.Calendar);
      } catch (error) {
        console.error(error);
      }
  };
  