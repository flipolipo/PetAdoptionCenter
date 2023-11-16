import {address_url} from './url.js';
import axios from "axios";

export  const fetchDataForShelter = async (shelterId) => {
    try {
      const responseData = await axios.get(
        `${address_url}/Shelters/${shelterId}`
      );
      return responseData.data;
    } catch (err) {
      console.log('shelter fetch error: ' + err);
    }
  };
