import React from 'react';
import './FooterStyle.css';

const Footer = () => {
  return (
    <div className="footer">
      <div className="heading">
        <h2>Small Palls</h2>
      </div>
      <div className="content">
        <div className="services">
          <h4>Services</h4>
          <p>
            <a href="#">App development</a>
          </p>
          <p>
            <a href="#">Web development</a>
          </p>
          <p>
            <a href="#">DevOps</a>
          </p>
          <p>
            <a href="#">Web designing</a>
          </p>
        </div>
        <div className="social-media">
          <h4>Social</h4>
          <p>
            <a href="#">
              <i className="fab fa-linkedin"></i> Linkedin
            </a>
          </p>
          <p>
            <a href="#">
              <i className="fab fa-twitter"></i> Twitter
            </a>
          </p>
          <p>
            <a href="https://github.com/farazc60">
              <i className="fab fa-github"></i> Github
            </a>
          </p>
          <p>
            <a href="https://www.facebook.com/codewithfaraz">
              <i className="fab fa-facebook"></i> Facebook
            </a>
          </p>
          <p>
            <a href="https://www.instagram.com/codewithfaraz">
              <i className="fab fa-instagram"></i> Instagram
            </a>
          </p>
        </div>
        <div className="links">
          <h4>Quick links</h4>
          <p>
            <a href="#">Home</a>
          </p>
          <p>
            <a href="#">About</a>
          </p>
          <p>
            <a href="#">Blogs</a>
          </p>
          <p>
            <a href="#">Contact</a>
          </p>
        </div>
        <div className="details">
          <h4 className="address">Address</h4>
          <p>
            Lorem ipsum dolor sit amet consectetur <br />
            adipisicing elit. Cupiditate, qui!
          </p>
          <h4 className="mobile">Mobile</h4>
          <p>
            <a href="#">+48 123123123</a>
          </p>
          <h4 className="mail">Join our newsletter!</h4>
          <div className="wrapper">
            <input
              className="footerInput"
              type="text"
              placeholder="Enter your email"
            />
            <button className="footerButton">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="28"
                height="28"
                viewBox="0 0 24 24"
                strokeWidth="2"
                stroke="currentColor"
                fill="none"
                strokeLinecap="round"
                strokeLinejoin="round"
              >
                <path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
                <path d="M5 12l14 0"></path>
                <path d="M13 18l6 -6"></path>
                <path d="M13 6l6 6"></path>
              </svg>
            </button>
          </div>
        </div>
      </div>
      <footer>
        <hr />
        Mozna tu coś napisać quote
      </footer>
    </div>
  );
};
export default Footer