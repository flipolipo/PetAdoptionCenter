import Model from 'react-modal'
import React, { useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';
import { address_url } from '../Service/url';

Modal.setAppElement('#root');

const Login = () => {

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

  async function loginUser() {
    try {
      const response = await axios.post(`${address_url}/Auth/Login`, {
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


  return (
    <div className='signInButton'>
      <button onClick={() => { setVisible(true); }}>Sign In</button>
      <Model isOpen={visible} onRequestClose={() => setVisible(false)} style={customStyles}>
        <input
          style={{ color: 'black' }}
          type="text"
          placeholder="Email"
          value={email}
          onChange={e => setEmail(e.target.value)}
        />
        <input
          style={{ color: 'black' }}
          type="password"
          placeholder="Password"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />
        <button onClick={loginUser}>Login</button>
        <button onClick={() => setVisible(false)}>Back</button>
      </Model>
    </div>
  )
}


export default Login