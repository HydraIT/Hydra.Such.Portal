﻿using Hydra.Such.Data.Database;
using Hydra.Such.Data.ViewModel;
using Hydra.Such.Data.ViewModel.CCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Hydra.Such.Data.Logic.CCP
{
    public static class DBProcedimentosCCP
    {
        #region User roles related to CCP process, available in the EnumerablesFixed class
        public const int _ElementoJuriPermission = 23;
        public const int _ElementoPreArea0 = 25;
        public const int _ElementoPreArea = 26;
        public const int _ElementoCompras = 27;
        public const int _ElementoJuri = 28;
        public const int _ElementoContabilidade = 29;
        public const int _ElementoJuridico = 30;
        public const int _ElementoCA = 31;
        public const int _GestorProcesso = 32;
        public const int _SecretariadoCA = 33;
        public const int _FechoProcesso = 34;
        public const int _ElementoArea = 37;
        #endregion

        // email address to use as sender
        public const string _EmailSender = "CCP_NAV@such.pt";

        #region CRUD Procedimentos
        public static List<ProcedimentosCcp> GetAllProcedimentosByProcedimentoTypeToList(int type)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                return _context.ProcedimentosCcp.Where(p => p.TipoProcedimento == type).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static List<ProcedimentosCcp> GetAllProcedimentosToList()
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                return _context.ProcedimentosCcp.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ProcedimentosCcp GetProcedimentoById(string ProcedimentoID)
        {
            var _context = new SuchDBContext();

            try
            {
                ProcedimentosCcp Procedimento = _context.ProcedimentosCcp.Where(p => p.Nº == ProcedimentoID).FirstOrDefault();

                if(Procedimento != null)
                {
                    /*
                        Every time a Procedimento CCP is retrieved from the database every other entity related to it must be also retrieved:
                              Tempos PA CCP
                              Registo de Actas
                              Elementos Juri
                              Emails Procedimento CCP
                              Linhas Para Encomenda Procedimentos CCP
                              Notas Procedimento CCP
                              Workflow Procedimentos CCP
                              Fluxo de Trabalho Lista Controlo
                    */ 
                    
                    Procedimento.TemposPaCcp = _context.TemposPaCcp.Where(t => t.NºProcedimento == Procedimento.Nº).FirstOrDefault();
                    Procedimento.RegistoDeAtas = GetAllRegistoDeActasProcedimento(Procedimento.Nº);
                    Procedimento.ElementosJuri = GetAllElementosJuriProcedimento(Procedimento.Nº);
                    Procedimento.EmailsProcedimentosCcp = GetAllEmailsProcedimento(Procedimento.Nº);
                    Procedimento.LinhasPEncomendaProcedimentosCcp = GetAllLinhasParaEncomenda(Procedimento.Nº);
                    Procedimento.NotasProcedimentosCcp = GetAllNotasProcedimento(Procedimento.Nº);
                    Procedimento.WorkflowProcedimentosCcp = GetAllWorkflowsProcedimento(Procedimento.Nº);
                    Procedimento.FluxoTrabalhoListaControlo = GetAllCheklistControloProcedimento(Procedimento.Nº);
                }
                
                return Procedimento;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        // zpgm - two overloaded methods to create ProcedimentosCcp: 
        //      the first uses a ProcedimentoCCPView object and returns a ProcedimentosCcp object
        //      the second uses a ProcedimentosCcp object and returns an object of the same type
        public static ProcedimentosCcp __CreateProcedimento(ProcedimentoCCPView Procedimento)
        {
            SuchDBContext _context = new SuchDBContext();
            ProcedimentosCcp proc = CCPFunctions.CastProcedimentoCcpViewToProcedimentoCcp(Procedimento);

            try
            {
                Configuração config = DBConfigurations.GetById(1);
                int NumeracaoProcedimento = 0;

                if (proc.TipoProcedimento == 1)
                {
                    NumeracaoProcedimento = config.NumeraçãoProcedimentoAquisição.Value;
                }
                else
                {
                    NumeracaoProcedimento = config.NumeraçãoProcedimentoSimplificado.Value;
                }

                proc.Nº = DBNumerationConfigurations.GetNextNumeration(NumeracaoProcedimento, true);
                proc.DataHoraCriação = DateTime.Now;
                proc.Estado = 0;

                proc.TemposPaCcp = new TemposPaCcp()
                {
                    NºProcedimento = proc.Nº,
                    DataHoraCriação = proc.DataHoraCriação,
                    UtilizadorCriação = proc.UtilizadorCriação,

                    Estado0 = 1,
                    Estado0Tg = 0,
                    Estado1 = 0,
                    Estado1Tg = 0,
                    Estado2 = 0,
                    Estado2Tg = 0,
                    Estado3 = 0,
                    Estado3Tg = 0,
                    Estado4 = 0,
                    Estado4Tg = 0,
                    Estado5 = 0,
                    Estado5Tg = 0,
                    Estado6 = 0,
                    Estado6Tg = 0,
                    Estado7 = 0,
                    Estado7Tg = 0,
                    Estado8 = 0,
                    Estado8Tg = 0,
                    Estado9 = 0,
                    Estado9Tg = 0,
                    Estado10 = 0,
                    Estado10Tg = 0,
                    Estado11 = 0,
                    Estado11Tg = 0,
                    Estado12 = 0,
                    Estado12Tg = 0,
                    Estado13 = 0,
                    Estado13Tg = 0,
                    Estado14 = 0,
                    Estado14Tg = 0,
                    Estado15 = 0,
                    Estado15Tg = 0,
                    Estado16 = 0,
                    Estado16Tg = 0,
                    Estado17 = 0,
                    Estado17Tg = 0,
                    Estado18 = 0,
                    Estado18Tg = 0,
                    Estado19 = 0,
                    Estado19Tg = 0,
                    Estado20 = 0,
                    Estado20Tg = 0
                };

                _context.Add(proc);
                _context.SaveChanges();

                //_context.Add(proc.TemposPaCcp);
                //_context.SaveChanges();
                
                ConfiguraçãoNumerações ConfigNum = DBNumerationConfigurations.GetById(NumeracaoProcedimento);
                ConfigNum.ÚltimoNºUsado = proc.Nº;
                DBNumerationConfigurations.Update(ConfigNum);

                return proc;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return null;
            }

        }
        public static ProcedimentosCcp __CreateProcedimento(ProcedimentosCcp Procedimento)
        {
            try
            {
                ProcedimentoCCPView ProcCCPView = CCPFunctions.CastProcedimentoCcpToProcedimentoCcpView(Procedimento);
                ProcedimentosCcp Proc = __CreateProcedimento(ProcCCPView);

                Procedimento.Nº = Proc.Nº;
                Procedimento.DataHoraCriação = Proc.DataHoraCriação;
                Procedimento.UtilizadorCriação = Proc.UtilizadorCriação;

                return Procedimento;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static ProcedimentosCcp __CreateProcedimento(int ProcedimentoType, string UserID)
        {
            try
            {
                ProcedimentoCCPView ProcedimentoView = new ProcedimentoCCPView
                {
                    TipoProcedimento = ProcedimentoType,
                    UtilizadorCriacao = UserID

                };

                ProcedimentosCcp Procedimento = __CreateProcedimento(ProcedimentoView);

                return Procedimento;
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public static ProcedimentosCcp __UpdateProcedimento(ProcedimentoCCPView Procedimento)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                ProcedimentosCcp proc = CCPFunctions.CastProcedimentoCcpViewToProcedimentoCcp(Procedimento);
                proc.DataHoraModificação = DateTime.Now;

                _context.ProcedimentosCcp.Update(proc);
                _context.SaveChanges();

                if (proc.ElementosJuri != null && proc.ElementosJuri.Count > 0)
                {
                    foreach (var ej in proc.ElementosJuri)
                    {
                        ej.DataHoraModificação = DateTime.Now;
                        ej.UtilizadorModificação = proc.UtilizadorModificação;

                        ElementosJuri Elemento = __UpdateElementoJuri(ej);
                    }

                }

                return proc;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static bool __DeleteProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.ElementosJuri.RemoveRange(_context.ElementosJuri.Where(ej => ej.NºProcedimento == ProcedimentoID));
                _context.NotasProcedimentosCcp.RemoveRange(_context.NotasProcedimentosCcp.Where(n => n.NºProcedimento == ProcedimentoID));
                _context.RegistoDeAtas.RemoveRange(_context.RegistoDeAtas.Where(a => a.NºProcedimento == ProcedimentoID));
                _context.WorkflowProcedimentosCcp.RemoveRange(_context.WorkflowProcedimentosCcp.Where(w => w.NºProcedimento == ProcedimentoID));
                _context.EmailsProcedimentosCcp.RemoveRange(_context.EmailsProcedimentosCcp.Where(ep => ep.NºProcedimento == ProcedimentoID));
                _context.LinhasPEncomendaProcedimentosCcp.RemoveRange(_context.LinhasPEncomendaProcedimentosCcp.Where(le => le.NºProcedimento == ProcedimentoID));
                _context.FluxoTrabalhoListaControlo.RemoveRange(_context.FluxoTrabalhoListaControlo.Where(f => f.No == ProcedimentoID));
                _context.TemposPaCcp.RemoveRange(_context.TemposPaCcp.Where(t => t.NºProcedimento == ProcedimentoID));

                _context.ProcedimentosCcp.RemoveRange(_context.ProcedimentosCcp.Where(p => p.Nº == ProcedimentoID));
                _context.SaveChanges();

                //__DeleteAllElementosJuriRelatedToProcedimento(ProcedimentoID);
                //__DeleteAllNotasProcedimentoRelatedToProcedimento(ProcedimentoID);
                //__DeleteAllRegistoDeAtasRelatedToProcedimento(ProcedimentoID);
                //__DeleteAllWorkflowsRelatedToProcedimento(ProcedimentoID);
                //__DeleteAllEmailsRelatedToProcedimento(ProcedimentoID);
                //__DeleteAllLinhasParaEncomendaRelatedToProcedimento(ProcedimentoID);
                //__DeleteAllCheklistControloRelatedToProcedimento(ProcedimentoID);
                //__DeleteTemposPaCcp(ProcedimentoID);
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region CRUD TemposPaCcp
        public static TemposPaCcp GetTemposPaCcP(string NoProcedimento)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                return _context.TemposPaCcp.Where(t => t.NºProcedimento == NoProcedimento).FirstOrDefault();
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public static bool __CreateTemposPaCcp(TemposPaCcp TemposPA)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.TemposPaCcp.Add(TemposPA);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
            
        }
        public static bool __UpdateTemposPaCcp(TemposPaCcp TemposPA)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.TemposPaCcp.Update(TemposPA);
                _context.SaveChanges();

                return true;

            }
            catch (Exception e)
            {

                return false;
            }
        }
        public static bool __DeleteTemposPaCcp(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                _context.TemposPaCcp.RemoveRange(_context.TemposPaCcp.Where(t => t.NºProcedimento == ProcedimentoID));

                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region CRUD RegistoDeActas
        public static string GetActaNumber(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            int num = _context.RegistoDeAtas.Where(a => a.NºProcedimento == ProcedimentoID).Count();

            num += 1;

            return num.ToString().PadLeft(4, '0');
        }
        public static List<RegistoDeAtas> GetAllRegistoDeActasProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                return _context.RegistoDeAtas.Where(a => a.NºProcedimento == ProcedimentoID).ToList();
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public static List<RegistoActasView> GetRegistosActasViewProcedimento(ProcedimentosCcp Procedimento)
        {
            List<RegistoActasView> RegistoView = new List<RegistoActasView>();
            foreach(var ra in Procedimento.RegistoDeAtas)
            {
                RegistoView.Add(CCPFunctions.CastRegistoActasToRegistoActasView(ra));
            }

            return RegistoView;
        }
        public static RegistoDeAtas __CreateRegistoDeAtas(ProcedimentosCcp Procedimento, bool SaveRecord)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                RegistoDeAtas Acta = new RegistoDeAtas()
                {
                    NºProcedimento = Procedimento.Nº,
                    NºAta = GetActaNumber(Procedimento.Nº),
                    DataHoraCriação = Procedimento.DataHoraCriação,
                    UtilizadorCriação = Procedimento.UtilizadorCriação
                };

                if (SaveRecord)
                {
                    _context.Add(Acta);
                    _context.SaveChanges();
                }
                
                return Acta;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static RegistoDeAtas __UpdateRegistoDeAtas(string ProcedimentoID, string NoActa, DateTime DataActa, string Observacoes, string ModificationUser, DateTime ModificationDate)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                RegistoDeAtas Acta = _context.RegistoDeAtas.Where(a => a.NºProcedimento == ProcedimentoID && a.NºAta == NoActa).FirstOrDefault();

                Acta.Observações = Observacoes;
                Acta.DataDaAta = DataActa;
                Acta.DataHoraModificação = ModificationDate;
                Acta.UtilizadorModificação = ModificationUser;

                _context.RegistoDeAtas.Update(Acta);
                _context.SaveChanges();

                return Acta;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static bool __DeleteAllRegistoDeAtasRelatedToProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                //RegistoDeAtas Acta = _context.RegistoDeAtas.Where(a => a.NºProcedimento == ProcedimentoID).FirstOrDefault();

                _context.RegistoDeAtas.RemoveRange(_context.RegistoDeAtas.Where(a => a.NºProcedimento == ProcedimentoID));
                _context.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public static bool __DeleteRegistoDeAtas(string ProcedimentoID, string NoActa)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                _context.RegistoDeAtas.RemoveRange(_context.RegistoDeAtas.Where(a => a.NºProcedimento == ProcedimentoID && a.NºAta == NoActa));
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region CRUD ElementosJuri
        public static List<ElementosJuri> GetAllElementosJuriProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                return _context.ElementosJuri.Where(ej => ej.NºProcedimento == ProcedimentoID).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static List<ElementosJuriView> GetAllElementosJuriViewProcedimento(ProcedimentosCcp Procedimento)
        {
            List<ElementosJuriView> ElementosView = new List<ElementosJuriView>();

            foreach(var e in Procedimento.ElementosJuri)
            {
                ElementosJuriView ElemJuriV = CCPFunctions.CastElementosJuriToElementosJuriView(e);
                if(e.Utilizador != null && e.Utilizador != "")
                    ElemJuriV.NomeEmpregado = GetUserName(ElemJuriV.Utilizador);

                ElementosView.Add(ElemJuriV);
            }

            return ElementosView;
        }

        public static ElementosJuri __CreateElementoJuri(ElementosJuriView ElementoView)
        {
            SuchDBContext _context = new SuchDBContext();

            if (ElementoView == null)
                return null;

            //ElementosJuri DuplicateElemento = _context.ElementosJuri.Where(ej => ej.NºProcedimento == ElementoView.NoProcedimento && ej.Utilizador == ElementoView.Utilizador).FirstOrDefault();

            //if (DuplicateElemento != null)
            //    return null;

            ElementosJuri Elemento = CCPFunctions.CastElementosJuriViewToElementosJuri(ElementoView);
            try
            {
                _context.Add(Elemento);
                _context.SaveChanges();

                return Elemento;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static ElementosJuri __UpdateElementoJuri(ElementosJuriView ElementoView)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                ElementosJuri Elemento = _context.ElementosJuri.Where(ej => ej.NºProcedimento == ElementoView.NoProcedimento && ej.NºLinha == ElementoView.NoLinha).FirstOrDefault();

                Elemento.Presidente = ElementoView.Presidente;
                Elemento.Vogal = ElementoView.Vogal;
                Elemento.Suplente = ElementoView.Suplente;
                Elemento.Email = ElementoView.Email;

                Elemento.DataHoraModificação = ElementoView.DataHoraModificacao;
                Elemento.UtilizadorModificação = ElementoView.UtilizadorModificacao;

                _context.Update(Elemento);
                _context.SaveChanges();

                return Elemento;
            }
            catch (Exception e)
            {

                return null;
            }

        }
        public static ElementosJuri __UpdateElementoJuri(ElementosJuri Elemento)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                ElementosJuri Elemento2 = _context.ElementosJuri.Where(ej => ej.NºProcedimento == Elemento.NºProcedimento && ej.NºLinha == Elemento.NºLinha).FirstOrDefault();

                Elemento2.Presidente = Elemento.Presidente;
                Elemento2.Vogal = Elemento.Vogal;
                Elemento2.Suplente = Elemento.Suplente;
                Elemento.Email = Elemento.Email;

                Elemento2.DataHoraModificação = Elemento.DataHoraModificação;
                Elemento2.UtilizadorModificação = Elemento.UtilizadorModificação;

                _context.Update(Elemento2);
                _context.SaveChanges();

                return Elemento2;
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public static bool __DeleteAllElementosJuriRelatedToProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.ElementosJuri.RemoveRange(_context.ElementosJuri.Where(ej => ej.NºProcedimento == ProcedimentoID));
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public static bool __DeleteElementoJuri(string ProcedimentoID, int LineNo)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                _context.ElementosJuri.RemoveRange(_context.ElementosJuri.Where(ej => ej.NºProcedimento == ProcedimentoID && ej.NºLinha == LineNo));
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        #endregion

        #region CRUD Emails Procedimentos CCP
        public static List<EmailsProcedimentosCcp> GetAllEmailsProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                return _context.EmailsProcedimentosCcp.Where(ep => ep.NºProcedimento == ProcedimentoID).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static List<EmailsProcedimentoCCPView> GetAllEmailsView(ProcedimentosCcp Procedimento)
        {
            List<EmailsProcedimentoCCPView> EmailsView = new List<EmailsProcedimentoCCPView>();
            foreach (var ep in Procedimento.EmailsProcedimentosCcp)
            {
                EmailsView.Add(CCPFunctions.CastEmailProcedimentoToEmailProcedimentoView(ep));
            }

            return EmailsView;
        }
        public static List<EmailsProcedimentoCCPView> GetAllEmailsView(string ProcedimentoID)
        {
            List<EmailsProcedimentosCcp> ProcedimentoEmails = GetAllEmailsProcedimento(ProcedimentoID);
            if(ProcedimentoEmails != null)
            {
                List<EmailsProcedimentoCCPView> EmailsViewList = new List<EmailsProcedimentoCCPView>();
                foreach(var ep in ProcedimentoEmails)
                {
                    EmailsViewList.Add(CCPFunctions.CastEmailProcedimentoToEmailProcedimentoView(ep));
                }

                return EmailsViewList;
            }
            return null;
        }

        public static bool __CreateEmailProcedimento(EmailsProcedimentosCcp Email)
        {
            if (Email == null)
                return false;

            SuchDBContext _context = new SuchDBContext();
            try
            {
                _context.Add(Email);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public static bool __UpdateEmailProcedimento(EmailsProcedimentosCcp Email)
        {
            if (Email == null)
                return false;

            SuchDBContext _context = new SuchDBContext();
            try
            {
                _context.Update(Email);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        public static bool __DeleteAllEmailsRelatedToProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.EmailsProcedimentosCcp.RemoveRange(_context.EmailsProcedimentosCcp.Where(ep => ep.NºProcedimento == ProcedimentoID));
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool __DeleteEmailProcedimento(string ProcedimentoID, int LineNo)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.EmailsProcedimentosCcp.RemoveRange(_context.EmailsProcedimentosCcp.Where(ep => ep.NºProcedimento == ProcedimentoID && ep.NºLinha == LineNo));
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region CRUD Linhas Para Encomenda CCP
        public static List<LinhasPEncomendaProcedimentosCcp> GetAllLinhasParaEncomenda(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                return _context.LinhasPEncomendaProcedimentosCcp.Where(le => le.NºProcedimento == ProcedimentoID).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static List<LinhasParaEncomendaCCPView> GetAllLinhasParaEncomendaView(ProcedimentosCcp Procedimento)
        {
            List<LinhasParaEncomendaCCPView> LinhasParaEncView = new List<LinhasParaEncomendaCCPView>();
            foreach (var le in Procedimento.LinhasPEncomendaProcedimentosCcp)
            {
                LinhasParaEncView.Add(CCPFunctions.CastLinhaParaEncomendaProcediementoToLinhaEncomendaCCPView(le));
            }

            return LinhasParaEncView;
        }

        public static bool __DeleteAllLinhasParaEncomendaRelatedToProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.LinhasPEncomendaProcedimentosCcp.RemoveRange(_context.LinhasPEncomendaProcedimentosCcp.Where(le => le.NºProcedimento == ProcedimentoID));
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool __DeleteLinhaParaEncomenda(string ProcedimentoID, int LineNo)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.LinhasPEncomendaProcedimentosCcp.RemoveRange(_context.LinhasPEncomendaProcedimentosCcp.Where(le => le.NºProcedimento == ProcedimentoID && le.NºLinha == LineNo));
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        //NR 20180226
        public static int GetMaxByLinhaParaEncomenda(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                int max = 0;

                LinhasPEncomendaProcedimentosCcp Linha = _context.LinhasPEncomendaProcedimentosCcp.Where(linha => linha.NºProcedimento == ProcedimentoID).LastOrDefault();
                if (Linha != null && Linha.NºLinha >= 0)
                    max = Linha.NºLinha + 1;
                else
                    max = max + 1;

                return max;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //NR 20180226
        public static LinhasPEncomendaProcedimentosCcp CreateLinhaProdutoServico(LinhasPEncomendaProcedimentosCcp ObjectToCreate)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.LinhasPEncomendaProcedimentosCcp.Add(ObjectToCreate);
                    ctx.SaveChanges();
                }

                return ObjectToCreate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //NR 20180227
        public static bool __DeleteLinhaProdutoServico(string ProcedimentoID, int LineNo)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                _context.LinhasPEncomendaProcedimentosCcp.RemoveRange(_context.LinhasPEncomendaProcedimentosCcp.Where(le => le.NºProcedimento == ProcedimentoID && le.NºLinha == LineNo));
                _context.SaveChanges();
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region CRUD Notas Procedimentos CCP
        public static List<NotasProcedimentosCcp> GetAllNotasProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                return _context.NotasProcedimentosCcp.Where(n => n.NºProcedimento == ProcedimentoID).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static List<NotasProcedimentoCCPView> GetAllNotasProcedimentoView(ProcedimentosCcp Procedimento)
        {
            List<NotasProcedimentoCCPView> NotasView = new List<NotasProcedimentoCCPView>();

            foreach(var n in Procedimento.NotasProcedimentosCcp)
            {
                NotasView.Add(CCPFunctions.CastNotaProcedimentoToNotaProcedimentoView(n));
            }

            return NotasView;
        }

        public static bool __DeleteAllNotasProcedimentoRelatedToProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.NotasProcedimentosCcp.RemoveRange(_context.NotasProcedimentosCcp.Where(n => n.NºProcedimento == ProcedimentoID));
                _context.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public static bool __DeleteNotaProcedimento(string ProcedimentoID, int LineNo)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                _context.NotasProcedimentosCcp.RemoveRange(_context.NotasProcedimentosCcp.Where(n => n.NºProcedimento == ProcedimentoID && n.NºLinha == LineNo));
                _context.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        #endregion

        #region CRUD Workflow Procedimentos CCP
        public static List<WorkflowProcedimentosCcp> GetAllWorkflowsProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                return _context.WorkflowProcedimentosCcp.Where(w => w.NºProcedimento == ProcedimentoID).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static List<WorkflowProcedimentosCCPView> GetAllWorkflowsView(ProcedimentosCcp Procedimento)
        {
            List<WorkflowProcedimentosCCPView> WorkflowsView = new List<WorkflowProcedimentosCCPView>();

            foreach(var w in Procedimento.WorkflowProcedimentosCcp)
            {
                WorkflowsView.Add(CCPFunctions.CastWorkflowProcedimentoToWorkflowProcedimentoView(w));
            }

            return WorkflowsView;
        }

        public static bool __DeleteAllWorkflowsRelatedToProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.WorkflowProcedimentosCcp.RemoveRange(_context.WorkflowProcedimentosCcp.Where(w => w.NºProcedimento == ProcedimentoID));
                _context.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public static bool __DeleteWorkflowsProcedimento(string ProcedimentoID, int State, DateTime DateTimeProc)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.WorkflowProcedimentosCcp.RemoveRange(_context.WorkflowProcedimentosCcp.Where(w => w.NºProcedimento == ProcedimentoID && w.Estado == State && w.DataHora == DateTimeProc));
                _context.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        #endregion

        #region CRUD Fluxo de Trabalho Lista Controlo
        public static List<FluxoTrabalhoListaControlo> GetAllCheklistControloProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                return _context.FluxoTrabalhoListaControlo.Where(f => f.No == ProcedimentoID).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static FluxoTrabalhoListaControlo GetChecklistControloProcedimento(string ProcedimentoID, int ProcedimentoState)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                return _context.FluxoTrabalhoListaControlo.Where(f => f.No == ProcedimentoID && f.Estado == ProcedimentoState).LastOrDefault();
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public static FluxoTrabalhoListaControlo GetChecklistControloProcedimento(string ProcedimentoID, int ProcedimentoState, DateTime FluxoDate, TimeSpan FluxoHour)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                return _context.FluxoTrabalhoListaControlo.Where(
                    f => f.No == ProcedimentoID && 
                    f.Estado == ProcedimentoState && 
                    f.Data == FluxoDate &&
                    f.Hora == FluxoHour).LastOrDefault();
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public static FluxoTrabalhoListaControlo __CreateFluxoTrabalho(string ProcedimentoID, DateTime SubmissionDate, int EstadoType, string Comment, string UserID, bool Imob)
        {
            SuchDBContext _context = new SuchDBContext();

            if (ProcedimentoID == "" || ProcedimentoID == null)
                return null;

            if (UserID == "")
                return null;
            
            FluxoTrabalhoListaControlo Fluxo = new FluxoTrabalhoListaControlo()
            {
                No = ProcedimentoID,
                Estado = 0,
                TipoEstado = EstadoType,
                Comentario = Comment,
                User = UserID
            };

            Fluxo.EstadoSeguinte = Imob ? 1 : 4;

            try
            {
                Fluxo.Data = SubmissionDate.Date;
                Fluxo.Hora = SubmissionDate.TimeOfDay;
            }catch(Exception e)
            {
                return null;
            }


            return Fluxo;
        }
        public static FluxoTrabalhoListaControlo __CreateFluxoTrabalho(FluxoTrabalhoListaControlo Fluxo)
        {
            SuchDBContext _context = new SuchDBContext();

            if (Fluxo == null)
                return null;

            try
            {
                _context.FluxoTrabalhoListaControlo.Add(Fluxo);
                _context.SaveChanges();

                return Fluxo;
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public static bool __UpdateFluxoTrabalho(FluxoTrabalhoListaControlo Fluxo)
        {
            if (Fluxo == null)
                return false;

            SuchDBContext _context = new SuchDBContext();
            try
            {
                _context.FluxoTrabalhoListaControlo.Update(Fluxo);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        public static bool __DeleteAllCheklistControloRelatedToProcedimento(string ProcedimentoID)
        {
            SuchDBContext _context = new SuchDBContext();

            try
            {
                _context.FluxoTrabalhoListaControlo.RemoveRange(_context.FluxoTrabalhoListaControlo.Where(f => f.No == ProcedimentoID));
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        public static bool __DeleteChecklistControlo(string ProcedimentoID, int State, DateTime Date, TimeSpan Time)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                _context.FluxoTrabalhoListaControlo.RemoveRange(_context.FluxoTrabalhoListaControlo.Where(f => f.No == ProcedimentoID && f.Estado == State && f.Data == Date && f.Hora == Time));
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        #endregion 

        #region parse ProcedimentosCCPView
        public static List<ProcedimentoCCPView> GetAllProcedimentosViewByProcedimentoTypeToList(int type)
        {
            List <ProcedimentosCcp> ProcList = GetAllProcedimentosByProcedimentoTypeToList(type);
            List<ProcedimentoCCPView> ProcViewList = new List<ProcedimentoCCPView>();

            if (ProcList == null)
                return null;

            try
            {
                foreach (var x in ProcList)
                {
                    ProcViewList.Add(CCPFunctions.CastProcedimentoCcpToProcedimentoCcpView(x));
                }

                return ProcViewList;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static List<ProcedimentoCCPView> GetAllProcedimentosByViewToList()
        {
            List<ProcedimentosCcp> ProcList = GetAllProcedimentosToList();
            List<ProcedimentoCCPView> ProcViewList = new List<ProcedimentoCCPView>();

            if (ProcList == null)
                return null;

            try
            {
                foreach (var x in ProcList)
                {
                    ProcViewList.Add(CCPFunctions.CastProcedimentoCcpToProcedimentoCcpView(x));
                }

                return ProcViewList;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static ProcedimentoCCPView GetProcedimentoCCPViewById(string id)
        {
            return CCPFunctions.CastProcedimentoCcpToProcedimentoCcpView(GetProcedimentoById(id));
        }
        #endregion

        #region Users settings related to Procedimentos CCP
        public static ConfiguracaoCcp GetConfiguracaoCCP()
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                return _context.ConfiguracaoCcp.Where(c => c.Id == 1).LastOrDefault();
                //return null;
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public static string GetUserName(string UserID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                var CU = _context.ConfigUtilizadores.Where(cu => cu.IdUtilizador == UserID).FirstOrDefault();
                return CU.Nome;
            }
            catch(Exception e)
            {
                return null;
            }

        }
        public static string GetUserEmail(string UserID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                var CU = _context.ConfigUtilizadores.Where(cu => cu.IdUtilizador == UserID).FirstOrDefault();
                if (EmailAutomation.IsValidEmail(CU.IdUtilizador))
                    return CU.IdUtilizador;
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static List<ConfigUtilizadores> GetAllUsersElementosJuri()
        {
            SuchDBContext _context = new SuchDBContext();
            List<AcessosUtilizador> UsersAccess = _context.AcessosUtilizador.Where(a => a.Funcionalidade == _ElementoJuriPermission).ToList();            

            List<ConfigUtilizadores> UsersElementosJuri = new List<ConfigUtilizadores>();

            try
            {
                foreach(var au in UsersAccess)
                {
                    UsersElementosJuri.Add(_context.ConfigUtilizadores.Where(u => u.IdUtilizador == au.IdUtilizador).FirstOrDefault());
                }
                return UsersElementosJuri;
            }
            catch(Exception e)
            {
                return null;
            }
        }    
        public static ConfigUtilizadores GetUserDetails(string UserID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                return _context.ConfigUtilizadores.Where(u => u.IdUtilizador == UserID).FirstOrDefault();
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public static List<AcessosUtilizador> GetUserAccesses(string UserID)
        {
            SuchDBContext _context = new SuchDBContext();
            try
            {
                return (_context.AcessosUtilizador.Where(a => a.IdUtilizador == UserID).ToList());
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static bool CheckUserRoleRelatedToCCP(string UserID, int RoleID)
        {
            List<AcessosUtilizador> UserAccessess = GetUserAccesses(UserID);
            AcessosUtilizador UserAcess = UserAccessess.Where(ua => ua.Funcionalidade == RoleID).FirstOrDefault();

            if (UserAcess != null)
                return true;

            return false;
        }
        #endregion

        #region Working days calculation
        // only excludes weekends
        public static int GetWorkingDays(this DateTime Current, DateTime FinishDateExclusive)
        {
            Func<int, bool> isWorkingDay = days =>
            {
                var currentDate = Current.AddDays(days);
                var isNonWorkingDay = currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday;
                return !isNonWorkingDay;
            };

            return Enumerable.Range(0, (Current - FinishDateExclusive).Days).Count(isWorkingDay);
        }

        // we can provide a list of dates to exclude from the range
        public static int GetWorkingDays(this DateTime Current, DateTime FinishDateExclusive, List<DateTime> ExcludedDates)
        {
            Func<int, bool> isWorkingDay = days =>
            {
                var currentDate = Current.AddDays(days);
                var isNonWorkingDay =
                    currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Sunday ||
                    ExcludedDates.Exists(excdate => excdate.Date.Equals(currentDate.Date));
                return !isNonWorkingDay;
            };

            return Enumerable.Range(0, (FinishDateExclusive - Current).Days).Count(isWorkingDay);
        }
        #endregion

        #region Processing Procedimentos
        // The following method maps NAV2009 ImobContabConfirmar(pEstado : Integer) function
        public static ErrorHandler AccountingConfirmsAssetPurchase(ProcedimentoCCPView Procedimento, ConfigUtilizadores UserDetails, int StateToCheck)
        {
            if (Procedimento.TemposPaCcp == null)
            {
                TemposPaCcp TemposPA = GetTemposPaCcP(Procedimento.No);
                if (TemposPA != null)
                {
                    // Holidays aren't excluded (see GetWorkingDays overload method thar uses a List<DateTime>)
                    TemposPA.Estado1Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                    TemposPA.UtilizadorModificação = UserDetails.IdUtilizador;
                    TemposPA.DataHoraModificação = DateTime.Now;

                    if (!__UpdateTemposPaCcp(TemposPA))
                    {
                        return ReturnHandlers.UnableToUpdateTemposPA;
                    }
                }
                else
                {
                    TemposPA.NºProcedimento = Procedimento.No;
                    TemposPA.Estado0 = 1;
                    TemposPA.Estado1 = 1;
                    TemposPA.Estado2 = 1;
                    TemposPA.Estado3 = 1;
                    TemposPA.Estado4 = 1;
                    TemposPA.Estado5 = 1;
                    TemposPA.Estado6 = 1;
                    TemposPA.Estado7 = 1;
                    TemposPA.Estado8 = 1;
                    TemposPA.Estado9 = 1;
                    TemposPA.Estado10 = 1;
                    TemposPA.Estado11 = 1;
                    TemposPA.Estado12 = 1;
                    TemposPA.Estado13 = 1;
                    TemposPA.Estado14 = 1;
                    TemposPA.Estado15 = 1;
                    TemposPA.Estado16 = 1;
                    TemposPA.Estado17 = 1;
                    TemposPA.Estado18 = 1;
                    TemposPA.Estado19 = 1;
                    TemposPA.Estado20 = 1;
                    TemposPA.Estado1Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value) + 1;
                    TemposPA.UtilizadorCriação = UserDetails.IdUtilizador;
                    TemposPA.DataHoraCriação = DateTime.Now;

                    if (!__CreateTemposPaCcp(TemposPA))
                    {
                        return ReturnHandlers.UnableToCreateTemposPA;
                    }
                }

                Procedimento.TemposPaCcp = CCPFunctions.CastTemposPaCcpToTemposCCPView(TemposPA);
            }
            else
            {
                Procedimento.TemposPaCcp.Estado1Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                Procedimento.TemposPaCcp.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.TemposPaCcp.DataHoraModificacao = DateTime.Now;
                if (!__UpdateTemposPaCcp(CCPFunctions.CastTemposCCPViewToTemposPaCcp(Procedimento.TemposPaCcp)))
                {
                    return ReturnHandlers.UnableToUpdateTemposPA;
                }
            }

            if (Procedimento.FluxoTrabalhoListaControlo == null)
            {
                FluxoTrabalhoListaControlo Fluxo0 = GetChecklistControloProcedimento(Procedimento.No, 0);
                if (Fluxo0 != null)
                {

                    Fluxo0.Resposta = Procedimento.ComentarioImobContabilidade;
                    Fluxo0.TipoResposta = Procedimento.Estado;
                    Fluxo0.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo0.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo0))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }
            else
            {
                FluxoTrabalhoListaControlo Fluxo0 = Procedimento.FluxoTrabalhoListaControlo.Where(s => s.Estado == 0).LastOrDefault();
                if (Fluxo0 != null)
                {
                    Fluxo0.Resposta = Procedimento.ComentarioImobContabilidade;
                    Fluxo0.TipoResposta = Procedimento.Estado;
                    Fluxo0.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo0.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo0))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }

            Procedimento.ImobilizadoSimNao = Procedimento.ImobilizadoSimNao.HasValue ? Procedimento.ImobilizadoSimNao : false;

            FluxoTrabalhoListaControlo NewFluxo1 = new FluxoTrabalhoListaControlo
            {
                No = Procedimento.No,
                Estado = 1,
                Data = DateTime.Now.Date,
                Hora = DateTime.Now.TimeOfDay,
                Comentario = Procedimento.ComentarioImobContabilidade,
                Comentario2 = Procedimento.ComentarioImobContabilidade2,
                ImobSimNao = Procedimento.ImobilizadoSimNao,
                User = UserDetails.IdUtilizador,
                NomeUser = UserDetails.Nome,
                TipoEstado = Procedimento.Estado,
                UtilizadorCriacao = UserDetails.IdUtilizador,
                DataHoraCriacao = DateTime.Now
            };

            if (StateToCheck == 1)
            {

                if (Procedimento.ImobilizadoSimNao.Value)
                    NewFluxo1.EstadoSeguinte = 4;
                else
                    NewFluxo1.EstadoSeguinte = 2;
            }
            else
            {
                NewFluxo1.EstadoSeguinte = 0;
            }

            if (__CreateFluxoTrabalho(NewFluxo1) == null)
            {
                return ReturnHandlers.UnableToCreateFluxo;
            }

            Procedimento.FluxoTrabalhoListaControlo = GetAllCheklistControloProcedimento(Procedimento.No);

            if (Procedimento.Estado == 1)
            {
                if (StateToCheck == 1)
                {
                    Procedimento.ImobilizadoSimNao = Procedimento.ImobilizadoSimNao.HasValue ? Procedimento.ImobilizadoSimNao : false;
                    if (Procedimento.ImobilizadoSimNao.Value)
                        Procedimento.Estado = 4;
                    else
                        Procedimento.Estado = 2;
                    Procedimento.ComentarioEstado = "";
                }
                else
                {
                    Procedimento.Estado = 0;
                    Procedimento.ComentarioEstado = Procedimento.ComentarioImobContabilidade;
                }

                if (Procedimento.TemposPaCcp.Estado1Tg - Procedimento.TemposPaCcp.Estado1 != 0)
                {
                    Procedimento.No_DiasAtraso = Procedimento.TemposPaCcp.Estado1Tg - Procedimento.TemposPaCcp.Estado1;
                    if (Procedimento.DataFechoPrevista.HasValue)
                    {
                        DateTime DateAux = Procedimento.DataFechoPrevista.Value;
                        Procedimento.DataFechoPrevista = DateAux.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                    else
                    {
                        Procedimento.DataFechoPrevista = DateTime.Now.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                }

                Procedimento.DataHoraEstado = DateTime.Now;
                Procedimento.UtilizadorEstado = UserDetails.IdUtilizador;
                Procedimento.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.DataHoraModificacao = DateTime.Now;

                if (__UpdateProcedimento(Procedimento) == null)
                {
                    return ReturnHandlers.UnableToUpdateProcedimento;
                }
            }

            return ReturnHandlers.Success;
        }
        // The following method maps NAV2009 ImobAreaConfirmar(pEstado : Integer) function
        public static ErrorHandler AreaConfirmsAssetPurchase(ProcedimentoCCPView Procedimento, ConfigUtilizadores UserDetails, int StateToCheck)
        {
            if(Procedimento.TemposPaCcp == null)
            {
                TemposPaCcp TemposPA = GetTemposPaCcP(Procedimento.No);
                if(TemposPA != null)
                {
                    // Holidays aren't excluded (see GetWorkingDays overload method thar uses a List<DateTime>)
                    TemposPA.Estado2Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                    TemposPA.UtilizadorModificação = UserDetails.IdUtilizador;
                    TemposPA.DataHoraModificação = DateTime.Now;

                    if (!__UpdateTemposPaCcp(TemposPA))
                    {
                        return ReturnHandlers.UnableToUpdateTemposPA;
                    }
                };

                Procedimento.TemposPaCcp = CCPFunctions.CastTemposPaCcpToTemposCCPView(TemposPA);
            }
            else
            {
                Procedimento.TemposPaCcp.Estado2Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                Procedimento.TemposPaCcp.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.TemposPaCcp.DataHoraModificacao = DateTime.Now;
                if (!__UpdateTemposPaCcp(CCPFunctions.CastTemposCCPViewToTemposPaCcp(Procedimento.TemposPaCcp)))
                {
                    return ReturnHandlers.UnableToUpdateTemposPA;
                }
            }

            if(Procedimento.FluxoTrabalhoListaControlo == null)
            {
                FluxoTrabalhoListaControlo Fluxo1 = GetChecklistControloProcedimento(Procedimento.No, 1);
                if(Fluxo1 != null)
                {
                    
                    Fluxo1.Resposta = Procedimento.ComentarioImobArea;
                    Fluxo1.TipoEstado = StateToCheck;
                    Fluxo1.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo1.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo1))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }
            else
            {
                FluxoTrabalhoListaControlo Fluxo1 = Procedimento.FluxoTrabalhoListaControlo.Where(s => s.Estado == 1).LastOrDefault();
                if (Fluxo1 != null)
                {
                    Fluxo1.Resposta = Procedimento.ComentarioImobContabilidade;
                    Fluxo1.TipoResposta = Procedimento.Estado;
                    Fluxo1.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo1.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo1))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }

            Procedimento.Imobilizado = Procedimento.Imobilizado.HasValue ? Procedimento.Imobilizado : false;

            FluxoTrabalhoListaControlo NewFluxo2 = new FluxoTrabalhoListaControlo
            {
                No = Procedimento.No,
                Estado = 2,
                Data = DateTime.Now.Date,
                Hora = DateTime.Now.TimeOfDay,
                Comentario = Procedimento.ComentarioImobArea,
                User = UserDetails.IdUtilizador,
                TipoEstado = Procedimento.Estado,
                NomeUser = UserDetails.Nome,
                UtilizadorCriacao = UserDetails.IdUtilizador,
                DataHoraCriacao = DateTime.Now,
                ImobSimNao = Procedimento.ImobilizadoSimNao
            };

            switch (StateToCheck)
            {
                case 1:
                    NewFluxo2.EstadoSeguinte = 3;
                    break;
                case 2:
                    NewFluxo2.EstadoSeguinte = 19;
                    break;
                case 0:
                    NewFluxo2.EstadoSeguinte = 1;
                    break;
            }

            if (__CreateFluxoTrabalho(NewFluxo2) == null)
            {
                return ReturnHandlers.UnableToCreateFluxo;
            }

            Procedimento.FluxoTrabalhoListaControlo = GetAllCheklistControloProcedimento(Procedimento.No);

            if(Procedimento.Estado == 2)
            {
                switch (StateToCheck)
                {
                    case 1:
                        Procedimento.Estado = 3;
                        Procedimento.ComentarioEstado = "";
                        break;
                    case 2:
                        Procedimento.Estado = 19;
                        Procedimento.ComentarioEstado = "";
                        break;
                    case 0:
                        Procedimento.Estado = 1;
                        Procedimento.ComentarioEstado = Procedimento.ComentarioImobArea;
                        break;
                }

                if(Procedimento.TemposPaCcp.Estado2Tg - Procedimento.TemposPaCcp.Estado2 != 0)
                {
                    Procedimento.No_DiasAtraso += (Procedimento.TemposPaCcp.Estado2Tg - Procedimento.TemposPaCcp.Estado2);
                    if (Procedimento.DataFechoPrevista.HasValue)
                    {
                        DateTime DateAux = Procedimento.DataFechoPrevista.Value;
                        Procedimento.DataFechoPrevista = DateAux.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                    else
                    {
                        Procedimento.DataFechoPrevista = DateTime.Now.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                };

                Procedimento.DataHoraEstado = DateTime.Now;
                Procedimento.UtilizadorEstado = UserDetails.IdUtilizador;
                Procedimento.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.DataHoraModificacao = DateTime.Now;

                if (__UpdateProcedimento(Procedimento) == null)
                {
                    return ReturnHandlers.UnableToUpdateProcedimento;
                };
            }

            return ReturnHandlers.Success;
        }
        // The following method maps NAV2009 FDComprasConfirmar(pEstado : Integer) function
        public static ErrorHandler DecisionGroundsToBuy(ProcedimentoCCPView Procedimento, ConfigUtilizadores UserDetails, int StateToCheck)
        {
            if (Procedimento.TemposPaCcp == null)
            {
                TemposPaCcp TemposPA = GetTemposPaCcP(Procedimento.No);
                if (TemposPA != null)
                {
                    // Holidays aren't excluded (see GetWorkingDays overload method thar uses a List<DateTime>)
                    TemposPA.Estado4Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                    TemposPA.UtilizadorModificação = UserDetails.IdUtilizador;
                    TemposPA.DataHoraModificação = DateTime.Now;

                    if (!__UpdateTemposPaCcp(TemposPA))
                    {
                        return ReturnHandlers.UnableToUpdateTemposPA;
                    }
                };

                Procedimento.TemposPaCcp = CCPFunctions.CastTemposPaCcpToTemposCCPView(TemposPA);
            }
            else
            {
                Procedimento.TemposPaCcp.Estado4Tg = Procedimento.TemposPaCcp.Estado4Tg ?? 0 + GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                Procedimento.TemposPaCcp.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.TemposPaCcp.DataHoraModificacao = DateTime.Now;
                if (!__UpdateTemposPaCcp(CCPFunctions.CastTemposCCPViewToTemposPaCcp(Procedimento.TemposPaCcp)))
                {
                    return ReturnHandlers.UnableToUpdateTemposPA;
                }
            }

            if (Procedimento.FluxoTrabalhoListaControlo == null)
            {
                FluxoTrabalhoListaControlo Fluxo2 = GetChecklistControloProcedimento(Procedimento.No, 2);
                if (Fluxo2 != null)
                {
                    Fluxo2.Resposta = Procedimento.ComentarioFundamentoCompras;
                    Fluxo2.TipoResposta = StateToCheck == 0 ? 0 : 1;
                    Fluxo2.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo2.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo2))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }
            else
            {
                FluxoTrabalhoListaControlo Fluxo2 = Procedimento.FluxoTrabalhoListaControlo.Where(s => s.Estado == 2).LastOrDefault();
                if (Fluxo2 != null)
                {
                    Fluxo2.Resposta = Procedimento.ComentarioFundamentoCompras;
                    Fluxo2.TipoResposta = StateToCheck == 0 ? 0 : 1;
                    Fluxo2.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo2.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo2))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }

            Procedimento.Imobilizado = Procedimento.Imobilizado.HasValue ? Procedimento.Imobilizado : false;

            FluxoTrabalhoListaControlo NewFluxo4 = new FluxoTrabalhoListaControlo
            {
                No = Procedimento.No,
                Estado = 4,
                Data = DateTime.Now.Date,
                Hora = DateTime.Now.TimeOfDay,
                Comentario = Procedimento.ComentarioFundamentoCompras,
                User = UserDetails.IdUtilizador,
                TipoEstado = 1,
                NomeUser = UserDetails.Nome,
                UtilizadorCriacao = UserDetails.IdUtilizador,
                DataHoraCriacao = DateTime.Now,
                ImobSimNao = Procedimento.ImobilizadoSimNao
            };

            switch (StateToCheck)
            {
                case 1:
                    NewFluxo4.EstadoSeguinte = 5;
                    break;
                case 2:
                    NewFluxo4.EstadoSeguinte = 6;
                    break;
                case 3:
                    NewFluxo4.EstadoSeguinte = 7;
                    break;
                case 4:
                    NewFluxo4.EstadoSeguinte = 8;
                    break;
                case 5:
                    NewFluxo4.EstadoSeguinte = 17;
                    break;
                case 9:
                    NewFluxo4.EstadoSeguinte = 0;
                    NewFluxo4.TipoEstado = 1;
                    break;
                case 0:
                    NewFluxo4.TipoEstado = 0;
                    NewFluxo4.EstadoSeguinte = Procedimento.Imobilizado.Value ? 2 : 0;
                    break;
            }

            if (__CreateFluxoTrabalho(NewFluxo4) == null)
            {
                return ReturnHandlers.UnableToCreateFluxo;
            }

            Procedimento.FluxoTrabalhoListaControlo = GetAllCheklistControloProcedimento(Procedimento.No);

            if(Procedimento.Estado > 3 && Procedimento.Estado < 7)
            {
                switch (StateToCheck)
                {
                    case 1:
                        Procedimento.Estado = 5;
                        Procedimento.WorkflowFinanceiros = true;
                        Procedimento.WorkflowFinanceirosConfirm = false;
                        Procedimento.ComentarioEstado = "";
                        break;
                    case 2:
                        Procedimento.Estado = 6;
                        Procedimento.WorkflowJuridicos = true;
                        Procedimento.WorkflowJuridicosConfirm = false;
                        Procedimento.ComentarioEstado = "";
                        break;
                    case 3:
                        Procedimento.Estado = 7;
                        Procedimento.ComentarioEstado = "";
                        break;
                    case 4:
                        Procedimento.Estado = 8;
                        Procedimento.ComentarioEstado = "";
                        break;
                    case 5:
                        Procedimento.Estado = 17;
                        Procedimento.ComentarioEstado = "";
                        break;
                    case 0:
                        Procedimento.Estado = Procedimento.Imobilizado.Value ? 2 : 0;
                        Procedimento.ComentarioEstado = Procedimento.ComentarioFundamentoCompras;
                        break;
                };

                if(Procedimento.TemposPaCcp.Estado4Tg - (Procedimento.TemposPaCcp.Estado4 ?? 0) != 0)
                {
                    Procedimento.No_DiasAtraso += (Procedimento.TemposPaCcp.Estado4Tg - Procedimento.TemposPaCcp.Estado4);
                    if (Procedimento.DataFechoPrevista.HasValue)
                    {
                        DateTime DateAux = Procedimento.DataFechoPrevista.Value;
                        Procedimento.DataFechoPrevista = DateAux.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                    else
                    {
                        Procedimento.DataFechoPrevista = DateTime.Now.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                }

                Procedimento.DataHoraEstado = DateTime.Now;
                Procedimento.UtilizadorEstado = UserDetails.IdUtilizador;

                Procedimento.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.DataHoraModificacao = DateTime.Now;

                if (__UpdateProcedimento(Procedimento) == null)
                {
                    return ReturnHandlers.UnableToUpdateProcedimento;
                };
            }

            return ReturnHandlers.Success;
        }
        // The following method maps NAV2009 FDFinancConfirmar(pEstado : Integer) function 
        public static ErrorHandler FinancialDecisionGroundsToBuy(ProcedimentoCCPView Procedimento, ConfigUtilizadores UserDetails, int StateToCheck)
        {
            if (Procedimento.TemposPaCcp == null)
            {
                TemposPaCcp TemposPA = GetTemposPaCcP(Procedimento.No);
                if (TemposPA != null)
                {
                    // Holidays aren't excluded (see GetWorkingDays overload method thar uses a List<DateTime>)
                    TemposPA.Estado5Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                    TemposPA.UtilizadorModificação = UserDetails.IdUtilizador;
                    TemposPA.DataHoraModificação = DateTime.Now;

                    if (!__UpdateTemposPaCcp(TemposPA))
                    {
                        return ReturnHandlers.UnableToUpdateTemposPA;
                    }
                };

                Procedimento.TemposPaCcp = CCPFunctions.CastTemposPaCcpToTemposCCPView(TemposPA);
            }
            else
            {
                Procedimento.TemposPaCcp.Estado5Tg = Procedimento.TemposPaCcp.Estado5Tg ?? 0 + GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                Procedimento.TemposPaCcp.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.TemposPaCcp.DataHoraModificacao = DateTime.Now;
                if (!__UpdateTemposPaCcp(CCPFunctions.CastTemposCCPViewToTemposPaCcp(Procedimento.TemposPaCcp)))
                {
                    return ReturnHandlers.UnableToUpdateTemposPA;
                }
            }
            if (Procedimento.FluxoTrabalhoListaControlo == null)
            {
                FluxoTrabalhoListaControlo Fluxo4 = GetChecklistControloProcedimento(Procedimento.No, 4);
                if (Fluxo4 != null)
                {
                    Fluxo4.Resposta = Procedimento.ComentarioFundamentoFinanceiros;
                    Fluxo4.TipoResposta = StateToCheck;
                    Fluxo4.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo4.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo4))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }
            else
            {
                FluxoTrabalhoListaControlo Fluxo4 = Procedimento.FluxoTrabalhoListaControlo.Where(s => s.Estado == 4).LastOrDefault();
                if (Fluxo4 != null)
                {
                    Fluxo4.Resposta = Procedimento.ComentarioFundamentoCompras;
                    Fluxo4.TipoResposta = StateToCheck;
                    Fluxo4.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo4.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo4))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }

            Procedimento.Imobilizado = Procedimento.Imobilizado.HasValue ? Procedimento.Imobilizado : false;

            FluxoTrabalhoListaControlo NewFluxo5 = new FluxoTrabalhoListaControlo
            {
                No = Procedimento.No,
                Estado = 5,
                Data = DateTime.Now.Date,
                Hora = DateTime.Now.TimeOfDay,
                Comentario = Procedimento.ComentarioFundamentoFinanceiros,
                Comentario2 = Procedimento.ComentarioFundamentoFinanceiros2,
                User = UserDetails.IdUtilizador,
                EstadoAnterior = 3, // JPM.08032018.Originally this value is hard-coded. Could it be used the property Estado value?
                TipoEstado = StateToCheck,
                NomeUser = UserDetails.Nome,
                UtilizadorCriacao = UserDetails.IdUtilizador,
                DataHoraCriacao = DateTime.Now,
                ImobSimNao = Procedimento.ImobilizadoSimNao
            };

            Procedimento.WorkflowJuridicosConfirm = Procedimento.WorkflowJuridicosConfirm.HasValue ? Procedimento.WorkflowJuridicosConfirm : false;
            Procedimento.WorkflowJuridicos = Procedimento.WorkflowJuridicos.HasValue ? Procedimento.WorkflowJuridicos : false;

            switch (StateToCheck)
            {
                case 1:
                    if(Procedimento.WorkflowJuridicosConfirm.Value)
                        NewFluxo5.EstadoSeguinte = 4;
                    else
                    {
                        if (Procedimento.WorkflowJuridicos.Value)
                            NewFluxo5.EstadoSeguinte = 6;
                        else
                            NewFluxo5.EstadoSeguinte = 4; 
                    }
                    break;
                case 9:
                    NewFluxo5.EstadoSeguinte = 0;
                    NewFluxo5.TipoEstado = 1;
                    break;
            }

            if (__CreateFluxoTrabalho(NewFluxo5) == null)
            {
                return ReturnHandlers.UnableToCreateFluxo;
            }

            if (Procedimento.Estado > 4 && Procedimento.Estado < 7)
            {
                if (StateToCheck == 1)
                {
                    if (Procedimento.WorkflowJuridicosConfirm.Value)
                        Procedimento.Estado = 4;
                    else
                    {
                        if (Procedimento.WorkflowJuridicos.Value)
                            Procedimento.Estado = 6;
                        else
                            Procedimento.Estado = 4;
                    }
                    Procedimento.ComentarioEstado = "";
                }
                else
                {
                    Procedimento.Estado = 4;
                    Procedimento.ComentarioEstado = Procedimento.ComentarioFundamentoFinanceiros;
                }

                #region MyRegion
                //  JPM.The original NAV code is depicted here using the TemposPaCcp properties to update the Procedimento closure date
                /* 
                 * 
                 * CalculaNDias();
                 * IF recTemposPA.GET("No.") THEN BEGIN
                 *      recTemposPA."Estado5 TG" += gNdias;
                 *      recTemposPA.MODIFY;
                 *      gDiferDias := recTemposPA."Estado5 TG" - recTemposPA."Estado5 TA";
                 * END;
                 * (...)
                 * 
                 * IF gDiferDias <> 0 THEN BEGIN
                 *      recCheckList.NoDiasAtraso += gDiferDias;
                 *      recCheckList.DataFechoPrev := recCheckList.DataFechoPrev + gDiferDias;
                 * END;
                 */

                #endregion
                if (Procedimento.TemposPaCcp.Estado5Tg - (Procedimento.TemposPaCcp.Estado5 ?? 0) != 0)
                {
                    Procedimento.No_DiasAtraso += (Procedimento.TemposPaCcp.Estado5Tg - Procedimento.TemposPaCcp.Estado5);
                    if (Procedimento.DataFechoPrevista.HasValue)
                    {
                        DateTime DateAux = Procedimento.DataFechoPrevista.Value;
                        Procedimento.DataFechoPrevista = DateAux.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                    else
                    {
                        Procedimento.DataFechoPrevista = DateTime.Now.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                }

                Procedimento.DataHoraEstado = DateTime.Now;
                Procedimento.UtilizadorEstado = UserDetails.IdUtilizador;

                Procedimento.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.DataHoraModificacao = DateTime.Now;

                if (__UpdateProcedimento(Procedimento) == null)
                {
                    return ReturnHandlers.UnableToUpdateProcedimento;
                };
            }
            
            return ReturnHandlers.Success;
        }
        // The following method maps NAV2009 FDAreaConfirmar(pEstado : Integer) function
        public static ErrorHandler AreaDecisionGroundsToBuy(ProcedimentoCCPView Procedimento, ConfigUtilizadores UserDetails, int StateToCheck)
        {
            if (Procedimento.TemposPaCcp == null)
            {
                TemposPaCcp TemposPA = GetTemposPaCcP(Procedimento.No);
                if (TemposPA != null)
                {
                    // Holidays aren't excluded (see GetWorkingDays overload method thar uses a List<DateTime>)
                    TemposPA.Estado7Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                    TemposPA.UtilizadorModificação = UserDetails.IdUtilizador;
                    TemposPA.DataHoraModificação = DateTime.Now;

                    if (!__UpdateTemposPaCcp(TemposPA))
                    {
                        return ReturnHandlers.UnableToUpdateTemposPA;
                    }
                };

                Procedimento.TemposPaCcp = CCPFunctions.CastTemposPaCcpToTemposCCPView(TemposPA);
            }
            else
            {
                Procedimento.TemposPaCcp.Estado7Tg = Procedimento.TemposPaCcp.Estado7Tg ?? 0 + GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                Procedimento.TemposPaCcp.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.TemposPaCcp.DataHoraModificacao = DateTime.Now;
                if (!__UpdateTemposPaCcp(CCPFunctions.CastTemposCCPViewToTemposPaCcp(Procedimento.TemposPaCcp)))
                {
                    return ReturnHandlers.UnableToUpdateTemposPA;
                }
            }

            if (Procedimento.FluxoTrabalhoListaControlo == null)
            {
                FluxoTrabalhoListaControlo Fluxo4 = GetChecklistControloProcedimento(Procedimento.No, 4);
                if (Fluxo4 != null)
                {
                    Fluxo4.Resposta = Procedimento.ComentarioFundamentoCompras;
                    Fluxo4.TipoResposta = StateToCheck;
                    Fluxo4.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo4.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo4))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }
            else
            {
                FluxoTrabalhoListaControlo Fluxo4 = Procedimento.FluxoTrabalhoListaControlo.Where(s => s.Estado == 4).LastOrDefault();
                if (Fluxo4 != null)
                {
                    Fluxo4.Resposta = Procedimento.ComentarioFundamentoCompras;
                    Fluxo4.TipoResposta = StateToCheck;
                    Fluxo4.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo4.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo4))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }

            Procedimento.Imobilizado = Procedimento.Imobilizado.HasValue ? Procedimento.Imobilizado : false;
            FluxoTrabalhoListaControlo NewFluxo7 = new FluxoTrabalhoListaControlo
            {
                No = Procedimento.No,
                Estado = 7,
                Data = DateTime.Now.Date,
                Hora = DateTime.Now.TimeOfDay,
                Comentario = Procedimento.ComentarioFundamentoCompras,
                User = UserDetails.IdUtilizador,
                TipoEstado = StateToCheck,
                NomeUser = UserDetails.Nome,
                UtilizadorCriacao = UserDetails.IdUtilizador,
                DataHoraCriacao = DateTime.Now,
                ImobSimNao = Procedimento.ImobilizadoSimNao
            };

            switch (StateToCheck)
            {
                case 1:
                    NewFluxo7.EstadoSeguinte = 8;
                    break;
                case 3:
                    NewFluxo7.EstadoSeguinte = 7;
                    break;
                case 9:
                    NewFluxo7.EstadoSeguinte = 0;
                    NewFluxo7.TipoEstado = 1;
                    break;
                case 0:
                    NewFluxo7.TipoEstado = 4;
                    break;
            }

            if (__CreateFluxoTrabalho(NewFluxo7) == null)
            {
                return ReturnHandlers.UnableToCreateFluxo;
            }

            Procedimento.FluxoTrabalhoListaControlo = GetAllCheklistControloProcedimento(Procedimento.No);

            if (Procedimento.Estado == 7)
            {
                switch (StateToCheck)
                {
                    case 1:
                        Procedimento.Estado = 8;     // Send to the Administration Board for approval
                        Procedimento.ComentarioEstado = "";
                        break;
                    case 3:
                        Procedimento.Estado = 19;   //  Closes the buying process
                        Procedimento.ComentarioEstado = Procedimento.ComentarioFundamentoCompras;
                        break;
                    case 0:
                        Procedimento.Estado = 4;    // Return the process to 
                        Procedimento.ComentarioEstado = Procedimento.ComentarioFundamentoCompras;
                        break;
                }

                if (Procedimento.TemposPaCcp.Estado7Tg - (Procedimento.TemposPaCcp.Estado7 ?? 0) != 0)
                {
                    Procedimento.No_DiasAtraso += (Procedimento.TemposPaCcp.Estado7Tg - Procedimento.TemposPaCcp.Estado4);
                    if (Procedimento.DataFechoPrevista.HasValue)
                    {
                        DateTime DateAux = Procedimento.DataFechoPrevista.Value;
                        Procedimento.DataFechoPrevista = DateAux.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                    else
                    {
                        Procedimento.DataFechoPrevista = DateTime.Now.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                }

                Procedimento.DataHoraEstado = DateTime.Now;
                Procedimento.UtilizadorEstado = UserDetails.IdUtilizador;

                Procedimento.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.DataHoraModificacao = DateTime.Now;

                if (__UpdateProcedimento(Procedimento) == null)
                {
                    return ReturnHandlers.UnableToUpdateProcedimento;
                };
            }

            return ReturnHandlers.Success;
        }
        // The following method maps NAV2009 Juridico1Fase(pEstado : Integer) function
        public static ErrorHandler LegalFirstPhase(ProcedimentoCCPView Procedimento, ConfigUtilizadores UserDetails, int StateToCheck)
        {
            if (Procedimento.TemposPaCcp == null)
            {
                TemposPaCcp TemposPA = GetTemposPaCcP(Procedimento.No);
                if (TemposPA != null)
                {
                    // Holidays aren't excluded (see GetWorkingDays overload method thar uses a List<DateTime>)
                    TemposPA.Estado6Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                    TemposPA.UtilizadorModificação = UserDetails.IdUtilizador;
                    TemposPA.DataHoraModificação = DateTime.Now;

                    if (!__UpdateTemposPaCcp(TemposPA))
                    {
                        return ReturnHandlers.UnableToUpdateTemposPA;
                    }
                };

                Procedimento.TemposPaCcp = CCPFunctions.CastTemposPaCcpToTemposCCPView(TemposPA);
            }
            else
            {
                Procedimento.TemposPaCcp.Estado6Tg = Procedimento.TemposPaCcp.Estado6Tg ?? 0 + GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                Procedimento.TemposPaCcp.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.TemposPaCcp.DataHoraModificacao = DateTime.Now;
                if (!__UpdateTemposPaCcp(CCPFunctions.CastTemposCCPViewToTemposPaCcp(Procedimento.TemposPaCcp)))
                {
                    return ReturnHandlers.UnableToUpdateTemposPA;
                }
            }

            if (Procedimento.FluxoTrabalhoListaControlo == null)
            {
                FluxoTrabalhoListaControlo Fluxo4 = GetChecklistControloProcedimento(Procedimento.No, 4);
                if (Fluxo4 != null)
                {
                    Fluxo4.Resposta = Procedimento.ComentarioJuridico6;
                    Fluxo4.TipoResposta = StateToCheck;
                    Fluxo4.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo4.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo4))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }
            else
            {
                FluxoTrabalhoListaControlo Fluxo4 = Procedimento.FluxoTrabalhoListaControlo.Where(s => s.Estado == 4).LastOrDefault();
                if (Fluxo4 != null)
                {
                    Fluxo4.Resposta = Procedimento.ComentarioJuridico6;
                    Fluxo4.TipoResposta = StateToCheck;
                    Fluxo4.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo4.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo4))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }

            Procedimento.Imobilizado = Procedimento.Imobilizado.HasValue ? Procedimento.Imobilizado : false;

            FluxoTrabalhoListaControlo NewFluxo6 = new FluxoTrabalhoListaControlo
            {
                No = Procedimento.No,
                Estado = 4,
                Data = DateTime.Now.Date,
                Hora = DateTime.Now.TimeOfDay,
                Comentario = Procedimento.ComentarioJuridico6,
                User = UserDetails.IdUtilizador,
                EstadoAnterior = 3,
                TipoEstado = StateToCheck,
                NomeUser = UserDetails.Nome,
                UtilizadorCriacao = UserDetails.IdUtilizador,
                DataHoraCriacao = DateTime.Now,
                ImobSimNao = Procedimento.ImobilizadoSimNao
            };

            Procedimento.WorkflowFinanceirosConfirm = Procedimento.WorkflowFinanceirosConfirm.HasValue ? Procedimento.WorkflowFinanceirosConfirm : false;
            Procedimento.WorkflowFinanceiros = Procedimento.WorkflowFinanceiros.HasValue ? Procedimento.WorkflowFinanceiros : false;

            if (Procedimento.WorkflowFinanceirosConfirm.Value)
                NewFluxo6.EstadoSeguinte = 4;
            else
            {
                if (Procedimento.WorkflowFinanceiros.Value)
                    NewFluxo6.EstadoSeguinte = 5;
                else
                    NewFluxo6.EstadoSeguinte = 4;
            }

            if (__CreateFluxoTrabalho(NewFluxo6) == null)
            {
                return ReturnHandlers.UnableToCreateFluxo;
            }

            Procedimento.FluxoTrabalhoListaControlo = GetAllCheklistControloProcedimento(Procedimento.No);

            if (Procedimento.Estado > 4 && Procedimento.Estado < 7)
            {
                if (StateToCheck == 1)
                {
                    if (Procedimento.WorkflowFinanceirosConfirm.Value)
                        Procedimento.Estado = 4;
                    else
                    {
                        if (Procedimento.WorkflowFinanceiros.Value)
                            Procedimento.Estado = 5;
                        else
                            Procedimento.Estado = 4;
                    }
                    Procedimento.ComentarioEstado = "";
                }
                else
                {
                    Procedimento.Estado = 4;
                    Procedimento.ComentarioEstado = Procedimento.ComentarioJuridico6;
                }

                if (Procedimento.TemposPaCcp.Estado6Tg - (Procedimento.TemposPaCcp.Estado6 ?? 0) != 0)
                {
                    Procedimento.No_DiasAtraso += (Procedimento.TemposPaCcp.Estado6Tg - Procedimento.TemposPaCcp.Estado6);
                    if (Procedimento.DataFechoPrevista.HasValue)
                    {
                        DateTime DateAux = Procedimento.DataFechoPrevista.Value;
                        Procedimento.DataFechoPrevista = DateAux.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                    else
                    {
                        Procedimento.DataFechoPrevista = DateTime.Now.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                }

                Procedimento.DataHoraEstado = DateTime.Now;
                Procedimento.UtilizadorEstado = UserDetails.IdUtilizador;

                Procedimento.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.DataHoraModificacao = DateTime.Now;

                if (__UpdateProcedimento(Procedimento) == null)
                {
                    return ReturnHandlers.UnableToUpdateProcedimento;
                };
            }

            return ReturnHandlers.Success;
        }
        // The following method maps NAV2009 Juridico2Fase(pEstado : Integer) function
        public static ErrorHandler LegalSecondPhase(ProcedimentoCCPView Procedimento, ConfigUtilizadores UserDetails, int StateToCheck)
        {
            if (Procedimento.TemposPaCcp == null)
            {
                TemposPaCcp TemposPA = GetTemposPaCcP(Procedimento.No);
                if (TemposPA != null)
                {
                    // Holidays aren't excluded (see GetWorkingDays overload method thar uses a List<DateTime>)
                    TemposPA.Estado14Tg += GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                    TemposPA.UtilizadorModificação = UserDetails.IdUtilizador;
                    TemposPA.DataHoraModificação = DateTime.Now;

                    if (!__UpdateTemposPaCcp(TemposPA))
                    {
                        return ReturnHandlers.UnableToUpdateTemposPA;
                    }
                };

                Procedimento.TemposPaCcp = CCPFunctions.CastTemposPaCcpToTemposCCPView(TemposPA);
            }
            else
            {
                Procedimento.TemposPaCcp.Estado14Tg = Procedimento.TemposPaCcp.Estado14Tg ?? 0 + GetWorkingDays(DateTime.Now, Procedimento.DataHoraEstado.Value);
                Procedimento.TemposPaCcp.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.TemposPaCcp.DataHoraModificacao = DateTime.Now;
                if (!__UpdateTemposPaCcp(CCPFunctions.CastTemposCCPViewToTemposPaCcp(Procedimento.TemposPaCcp)))
                {
                    return ReturnHandlers.UnableToUpdateTemposPA;
                }
            }

            if (Procedimento.FluxoTrabalhoListaControlo == null)
            {
                FluxoTrabalhoListaControlo Fluxo13 = GetChecklistControloProcedimento(Procedimento.No, 13);
                if (Fluxo13 != null)
                {
                    Fluxo13.Resposta = Procedimento.ComentarioJuridico14;
                    Fluxo13.TipoResposta = StateToCheck;
                    Fluxo13.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo13.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo13))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }
            else
            {
                FluxoTrabalhoListaControlo Fluxo13 = Procedimento.FluxoTrabalhoListaControlo.Where(s => s.Estado == 13).LastOrDefault();
                if (Fluxo13 != null)
                {
                    Fluxo13.Resposta = Procedimento.ComentarioJuridico14;
                    Fluxo13.TipoResposta = StateToCheck;
                    Fluxo13.UtilizadorModificacao = UserDetails.IdUtilizador;
                    Fluxo13.DataHoraModificacao = DateTime.Now;

                    if (!__UpdateFluxoTrabalho(Fluxo13))
                    {
                        return ReturnHandlers.UnableToUpdateFluxo;
                    }
                }
            }

            Procedimento.Imobilizado = Procedimento.Imobilizado.HasValue ? Procedimento.Imobilizado : false;

            FluxoTrabalhoListaControlo NewFluxo14 = new FluxoTrabalhoListaControlo
            {
                No = Procedimento.No,
                Estado = 14,
                Data = DateTime.Now.Date,
                Hora = DateTime.Now.TimeOfDay,
                Comentario = Procedimento.ComentarioJuridico14,
                User = UserDetails.IdUtilizador,
                TipoEstado = StateToCheck,
                EstadoSeguinte = 15,
                NomeUser = UserDetails.Nome,
                UtilizadorCriacao = UserDetails.IdUtilizador,
                DataHoraCriacao = DateTime.Now,
                ImobSimNao = Procedimento.ImobilizadoSimNao
            };

            if (__CreateFluxoTrabalho(NewFluxo14) == null)
            {
                return ReturnHandlers.UnableToCreateFluxo;
            }

            Procedimento.FluxoTrabalhoListaControlo = GetAllCheklistControloProcedimento(Procedimento.No);

            if (Procedimento.Estado == 14)
            {
                if (StateToCheck == 1)
                {
                    Procedimento.Estado = 15;   // JPM.Sends to Purchaseing Departement
                    Procedimento.ComentarioEstado = "";
                }
                else
                {
                    Procedimento.Estado = 13; // JPM.The NAV original code denotes uncertainty in this attribution 
                    Procedimento.ComentarioEstado = Procedimento.ComentarioJuridico14;
                }

                if (Procedimento.TemposPaCcp.Estado14Tg - (Procedimento.TemposPaCcp.Estado14 ?? 0) != 0)
                {
                    Procedimento.No_DiasAtraso += (Procedimento.TemposPaCcp.Estado14Tg - Procedimento.TemposPaCcp.Estado14);
                    if (Procedimento.DataFechoPrevista.HasValue)
                    {
                        DateTime DateAux = Procedimento.DataFechoPrevista.Value;
                        Procedimento.DataFechoPrevista = DateAux.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                    else
                    {
                        Procedimento.DataFechoPrevista = DateTime.Now.AddDays(Procedimento.No_DiasAtraso.Value);
                    }
                }

                Procedimento.DataHoraEstado = DateTime.Now;
                Procedimento.UtilizadorEstado = UserDetails.IdUtilizador;

                Procedimento.UtilizadorModificacao = UserDetails.IdUtilizador;
                Procedimento.DataHoraModificacao = DateTime.Now;

                if (__UpdateProcedimento(Procedimento) == null)
                {
                    return ReturnHandlers.UnableToUpdateProcedimento;
                };
            }

            return ReturnHandlers.Success;
        }
        #endregion

    }
}
