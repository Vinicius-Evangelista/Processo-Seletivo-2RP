import React from 'react';

//Imagens
import Logo from '../../Assets/Images/Logo-2RP.svg'

//Css
import '../../Assets/Css/index.css';

export default function HeaderLogin() {

    return (
        <div>
            <header>
                {/* <!-- Grid --> */}
                <div className="box-header container">

                    {/* <!-- Logo --> */}
                    <img src={Logo} alt="2RP Logo" className='logo' />
                </div>
            </header>
        </div>
    );
}