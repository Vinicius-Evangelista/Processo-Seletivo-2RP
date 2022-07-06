import React from 'react';

//Imagens
import Icone_facebook from '../../Assets/Images/logo-facebook.svg';
import Icone_youtube from '../../Assets/Images/logo-youtube.svg';
import Icone_linkedin from '../../Assets/Images/LinkedIn.svg';
import Logo from '../../Assets/Images/Logo-2RP.svg'

//Css
import '../../Assets/Css/index.css';
import { Link } from 'react-router-dom';


export default function Footer() {
    return (
        <div>
            <footer>
                <div className="container_footer container">
                    <img className='logo' src={Logo} alt="2RP Logo" />
                    <small className='small_container_footer'>© 2RP Net - Todos os direitos reservados</small>
                    <div className='box_redes_socias_footer'>
                        <Link to="">
                            <img className='icone_footer' src={Icone_facebook} alt="2RP Logo" />
                        </Link >

                        <Link to="">
                            <img className='icone_footer' src={Icone_youtube} alt="2RP Logo" />
                        </Link>

                        <Link to="">
                            <img className='icone_footer' src={Icone_linkedin} alt="2RP Logo" />
                        </Link>
                    </div>
                </div>
            </footer>
        </div>
    );
}