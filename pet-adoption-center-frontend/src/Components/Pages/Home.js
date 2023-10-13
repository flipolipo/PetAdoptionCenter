import React, {useEffect} from 'react';
import redPaw from '../../Photo/redPaw.png';
import whitePaw from '../../Photo/whitePaw.png';
import pets from '../../Photo/pets.png';
import './Home.css';
import { Link } from 'react-router-dom';
import { fetchData } from '../../Service/apiGetData';

const Home = () => {
  useEffect(() => {
    fetchPetsAvailableForAdoption();
  }, []);

  function fetchPetsAvailableForAdoption() {
    fetchData('Users/pets/available-to-adoption')
      .then((data) => {
        console.log(data);
      })
      .catch((error) => console.log(error));
  }
 
  return (
    <div className="homepage-container">
    <div className="pets">
        <img className="image-9-icon" alt="" src={pets}/>
        <div className='rectanglePets'>
          <Link to="/Users/pets" className="find-your-new">Find your new best friend</Link>
        </div>
    </div>
  </div>
  )
}
export default Home