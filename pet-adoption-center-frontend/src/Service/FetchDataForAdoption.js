import {address_url} from './url.js';
import axios from "axios";

export const FetchDataForAdoption = async (adoptionId) => {
    try {
        const response = await axios.get(`${address_url}/adoptions/${adoptionId}`);
        return (response.data);
      } catch (error) {
        console.error(error);
      }
}