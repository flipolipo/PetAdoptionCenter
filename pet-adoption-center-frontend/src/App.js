import './App.css';
import  Home   from './Components/Home';
import { Shelters } from './Components/Shelters';
import { Adoption } from './Components/Adoption';
import { TemporaryHouse } from './Components/TemporaryHouse';
import { Pets } from './Components/Pets';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './Components/Login';
import Register from './Components/Register';
import Navbar from './Components/Navbar';
function App() {
  return (
    <Router>
    <div className="App-container">
      <Navbar/>
   <div className='routes'>
   <Routes>
    <Route exact path="/" element={<Home/>}/>
    <Route path="/Shelters" element={<Shelters/>}/>
    <Route path="/Shelters/adoptions" element={<Adoption/>}/>
    <Route path="/Shelters/temporaryHouses" element={<TemporaryHouse/>}/>
    <Route path="/Users/pets" element={<Pets/>}/>
    <Route path="/sign-in" element={<Login/>}/>
    <Route path="/sign-up" element={<Register/>}/>
   </Routes>
   </div>
    </div>
    </Router>
  );
}

export default App;
