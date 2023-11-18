import {address_url} from './url.js';
import axios from "axios";

export const FetchTempHouseDataForUser = async (userId) => {
    try {
        const responseData = await axios.get(
          `${address_url}/Users/${userId}/tempHouses`
        );
        return responseData.data;
      } catch (err) {
        console.log('shelter fetch error: ' + err);
      }
}
