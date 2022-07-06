//Hooks
import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import toast, { Toaster } from 'react-hot-toast';

//Serviços
import api from '../../Services/api';

//Components
import Header from '../../Components/Header';
import Foooter from '../../Components/Footer';
import Sidebar from '../../Components/Sidebar'


//Css
import "../../Assets/Css/index.css";
import { parseJwt } from '../../Services/auth';

export default function AtualizarUsuario() {

    let { userId } = useParams();

    //States
    const [nome, setNome] = useState("")
    const [email, setEmail] = useState("")
    const [senha, setSenha] = useState("")
    const [status, setStatus] = useState(true)
    const [tipoUsuario, setTipoUsuario] = useState(0)
    const [listaTiposUsuarios, setListaTiposUsuario] = useState([])
    const [usuarioAchado, setUsuarioAchado] = useState({})
    const AvisoErro = () => toast.error('Algo deu errado. Revise os campos do usuário!');
    const AvisoSucesso = () => toast.success('Usuário atualizado com sucesso!');



    const AtualizarUsuarioGeral = (event) => {

        event.preventDefault();

        var formData = new FormData();

        const element = document.getElementById('fotoCadastro')
        const file = element.files[0]

        if (element.value == "") {
            formData.append('FotoPerfil', null)

        } else {
            formData.append('FotoPerfil', file, file.name)

        }

        formData.append('Nome', nome);
        formData.append('Email', email);
        formData.append('Senha', senha);

        api.put(`/Usuarios/Atualizar/Geral/${userId}`, formData, {

            Authorization: 'Bearer ' + localStorage.getItem('usuario-login'),
            headers: { "Content-Type": "multipart/form-data" }
        })
            .then((resposta) => {

                if (resposta.status === 200) {
                    AvisoSucesso();
                }
            })
            .catch((erro) => AvisoErro())

    }


    function BuscarTipoUsuario() {
        api.get('/TiposUsuarios/Listar', {
            headers: {

                Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
            }
        }
        )
            .then((resposta) => {
                if (resposta.status === 200) {
                    setListaTiposUsuario(resposta.data)
                }
            })
            .catch(erro => console.log(erro))
    }

    useEffect(() => BuscarTipoUsuario(), [])


    return (
        <div>
            <Toaster
                position='top-right'
                toastOptions={{
                    style: {
                        fontFamily: "'Exo', sans-serif",
                        fontSize: "1rem",
                        fontWeight: 500
                    }
                }}
            />
            <Header />
            <Sidebar />
            {/* <!-- Main --> */}
            <main>
                <div className="container_cadastro_usuario container">
                    <form className='form_cadastro_usuario' onSubmit={AtualizarUsuarioGeral}>
                        <label htmlFor="fotoCadastro" className='label_cadastro_usuario'>Selecionar Foto</label>
                        <input
                            type="file"
                            className='input_cadastro_usuario'
                            id="fotoCadastro"
                            name="fotoCadastro"
                            />
                        <label></label>
                        <input type="text"
                            onChange={(event) => setNome(event.target.value)}
                            placeholder={parseJwt().nome} value={nome} className="input_login"
                            required
                        />
                        <label></label>
                        <input type="text" placeholder={parseJwt().email} value={email}
                            onChange={(event) => setEmail(event.target.value)}
                            className="input_login"
                            required
                            />
                        <input type="text" placeholder="**********" value={senha}
                            onChange={(event) => setSenha(event.target.value)}
                            className="input_login" 
                            required
                            />
                        <button type="submit"  className="button_login">Atualizar</button>
                    </form>
                </div>
            </main>
            <Foooter />
        </div>
    );
}