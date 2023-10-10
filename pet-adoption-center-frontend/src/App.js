import './App.css';
import { Home }  from './Components/Home';
import { Shelters } from './Components/Shelters';
import { Adoption } from './Components/Adoption';
import { TemporaryHouse } from './Components/TemporaryHouse';
import { Pets } from './Components/Pets';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { NavbarLogo } from './Components/NavbarLogo';
import { NavbarNavigation } from './Components/NavbarNavigation';
function App() {
  return (
    <Router>
    <div className="App-container">
      <div className='navbar'>
   <NavbarLogo/>
   </div><div className='navbarNavig'>
   <NavbarNavigation/>
   </div>
   <div className='routes'>
   <Routes>
    <Route exact path="/" element={<Home/>}/>
    <Route path="/Shelters" element={<Shelters/>}/>
    <Route path="/Shelters/adoptions" element={<Adoption/>}/>
    <Route path="/Shelters/temporaryHouses" element={<TemporaryHouse/>}/>
    <Route path="/Users/pets" element={<Pets/>}/>
   </Routes>
   </div>
    </div>
    </Router>
  );
}

export default App;
