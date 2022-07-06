//Hooks
import { useEffect, useState } from 'react';
import { Link, useNavigate } from "react-router-dom";
import toast, { Toaster } from 'react-hot-toast';

//Serviços
import { parseJwt } from '../../Services/auth';
import api from '../../Services/api';

//Imagens
import icone_email from '../../Assets/Images/Email.svg';
import icone_inativo from '../../Assets/Images/inativo.svg';
import icone_ativo from '../../Assets/Images/ativo.svg';
import seta_direita from '../../Assets/Images/seta-direita.svg';
import seta_esquerda from '../../Assets/Images/seta-esquerda.svg';


//Components
import Header from '../../Components/Header';
import Foooter from '../../Components/Footer';
import Sidebar from '../../Components/Sidebar';
import Modal from 'react-modal';


//Css
import "../../Assets/Css/index.css";

export default function Perfis() {

    const [listaUsuario, setListaUsuario] = useState([])
    const [usuario, setUsuario] = useState({})
    const [posicaoUsuario, setPosicaoUsuario] = useState(0)
    const [modalIsOpen, setIsOpen] = useState(false)
    const [listaTiposUsuarios, setListaTiposUsuario] = useState([])
    const AvisoErro = () => toast.error('Algo deu errado. Verifique sua conexão com a internet!')
    const AvisoSucesso = () => toast.success('Usuário excluido com sucesso!')


    function closeModal() {
        setIsOpen(!modalIsOpen);
    }


    function ExcluirUsuario() {
        api.delete(`/Usuarios/Excluir/${usuario.idUsuario}`, {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
            }

        })
            .then((resposta) => {

                if (resposta.status === 204) {
                    AvisoSucesso()
                    BuscarUsuarios()
                }
            })

            .catch(erro => AvisoErro())

    }

    function BuscarUsuarios() {
        api.get("/Usuarios/Listar", {
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
            }

        })
            .then((resposta) => {

                if (resposta.status === 200) {

                    console.log(resposta.data)
                    setListaUsuario(resposta.data)
                    resposta.data.map((u) => {
                        if (u.idUsuario == parseJwt().jti) {

                            setPosicaoUsuario(resposta.data.indexOf(u))
                            return setUsuario(u);
                        }
                    })
                }
            })

            .catch(erro => console.log(erro))

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


    useEffect(() => BuscarUsuarios(), [])
    useEffect(() => BuscarTipoUsuario(), [])

    // Passa para o próximo usuário dentro da lista
    function ProximoUsuario() {
        if ((posicaoUsuario + 1) <= (listaUsuario.length - 1)) {  //Verificação para ver se existe um próximo usuário
            let posicao = posicaoUsuario
            setUsuario(listaUsuario[posicao + 1])
            setPosicaoUsuario((posicaoUsuario + 1))
            console.log(posicaoUsuario)
        }
    }

    // Volta para o usuário anterior dentro da lista
    async function UsuarioAnterior() {

        if ((posicaoUsuario - 1) >= 0) {  //Verificação para ver se existe um usuario anterior
            let posicao = posicaoUsuario
            setUsuario(listaUsuario[posicao - 1])
            setPosicaoUsuario((posicaoUsuario - 1))
            console.log(posicaoUsuario)
        }
    }



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
            <Modal
                className={"Modal"}
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                overlayClassName="Overlay"

            >
                <div className='container_modal_perfil'>
                    <img className='foto_perfil_modal_perfis' src={"http://localhost:5000/StaticFiles/UserImages/" + usuario.foto} />
                    <span className='span_modal_perfil'>Tem certeza de deseja remover esse usuário ?</span>

                    <button style={{"background-color": "#BA3333"}} className='button_perfil' onClick={() => ExcluirUsuario()}>Excluir</button>
                    <button className='button_modal_perfil' onClick={() => closeModal()}>Fechar</button>

                </div>
            </Modal>
            <Header />
            <Sidebar />
            {/* <!-- Main --> */}
            <main>
                <div className="container_perfil container">
                    <div className="box_perfil">
                        <img className='foto_perfil_perfis' src={"http://localhost:5000/StaticFiles/UserImages/" + usuario.foto} />
                        <div className='box_nomes_perfil'>
                            <span className='nome_usuario_perfil'>{usuario.nome}</span>
                            {listaTiposUsuarios.map((t) => {
                                if(usuario.idTipoUsuario == t.idTipoUsuario)
                                {
                                    return <span className='tipo_usuario_perfil'>Usuario {t.nomeTipoUsuario}</span>
                                }
                            })}
                        </div>
                        <div className="span_perfil">
                            <img src={icone_email} className="icone_perfil" />
                            <span>{usuario.email}</span>
                        </div>

                        <div className="span_perfil">
                            <img className="icone_perfil" src={usuario.status == true ? icone_ativo : icone_inativo} />
                            <span>{usuario.status == true ? "Ativo" : "Inativo"}</span>
                        </div>
                        {parseJwt().role == "1" || parseJwt().role == "2"?
                            <div className='box_setas_perfis'>
                                <button id='seta_perfil_esquerda' onClick={() => UsuarioAnterior()} className='seta_perfil'>
                                    <img src={seta_esquerda} />
                                </button>
                                <Link to={`/AtualizarUsuarioPrivilegiado/${usuario.idUsuario}`} className='button_perfil'>Atualizar Perfil</Link>
                                {parseJwt().role == "1" && <button className='button_perfil' onClick={closeModal}>Excluir</button> }
                                <button id='seta_perfil_direita' onClick={() => ProximoUsuario()} className='seta_perfil'>
                                    <img src={seta_direita} />
                                </button>
                            </div>
                            :
                            <Link to={`/AtualizarUsuarioGeral/${usuario.idUsuario}`} className='button_perfil_geral'>Atualizar Perfil</Link>
                        }
                    </div>
                </div>
            </main>
            <Foooter />
        </div>
    );
}