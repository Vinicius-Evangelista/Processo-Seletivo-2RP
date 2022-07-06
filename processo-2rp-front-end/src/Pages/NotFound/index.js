import React from 'react';

//Imagens
import Logo from '../../Assets/Images/Logo-2RP.svg'

//Css
import '../../Assets/Css/index.css';

export default function NotFound() {

    return (
        <div>
            <main>
                {/* <!-- Grid --> */}
                <div className="container_not_found">

                    {/* <!-- Logo --> */}
                    <img src={Logo} alt="2RP Logo" className='logo' />
                    <span className='span_not_found'>4O4 - Not Found</span>
                </div>
            </main>
        </div>
    );
}