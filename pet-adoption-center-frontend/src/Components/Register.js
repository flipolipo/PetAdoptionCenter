
import Model from 'react-modal'
import React, { useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';
import { address_url } from '../Service/url';

Modal.setAppElement('#root');



const Register = () => {

  const customStyles = {
    content: {
      top: '50%',
      left: '50%',
      right: 'auto',
      bottom: 'auto',
      marginRight: '-50%',
      transform: 'translate(-50%, -50%)',
    },
  };


  async function registerUser() {
    try {
      const response = await axios.post(`${address_url}/Auth/Register`, {
        Username: userName,
        Email: email,
        Password: password
      });
      console.log(response.data);

    } catch (error) {
      console.error(error);

    }
  }


  const [visible, setVisible] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userName, setUserName] = useState('');


  return (
    <div className='signUpButton'>
      <button onClick={() => { setVisible(true); }}>Sign Up</button>
      <Model isOpen={visible} onRequestClose={() => setVisible(false)} style={customStyles}>

        <input
          type="username"
          placeholder="Username"
          value={userName}
          onChange={e => setUserName(e.target.value)}
        />

        <input
          type="text"
          placeholder="Email"
          value={email}
          onChange={e => setEmail(e.target.value)}
        />

        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />


        <button onClick={registerUser}>Register</button>


        <button onClick={() => setVisible(false)}>Back</button>
      </Model>
    </div>
  )
}


export default Register