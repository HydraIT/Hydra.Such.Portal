﻿using System;
using System.Collections.Generic;
using System.Text;
using Hydra.Such.Data.Database;
using System.Linq;
using Hydra.Such.Data.ViewModel;

namespace Hydra.Such.Data.Logic
{
    public static class DBContacts
    {
        #region CRUD
        public static Contactos GetById(string id)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.Contactos.Where(x => x.No == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<Contactos> GetAll()
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    return ctx.Contactos.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        
        public static Contactos Create(Contactos item)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    item.DataCriacao = DateTime.Now;
                    ctx.Contactos.Add(item);
                    ctx.SaveChanges();
                }
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<Contactos> Create(List<Contactos> items)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    items.ForEach(x =>
                        {
                            x.DataCriacao = DateTime.Now;
                            ctx.Contactos.Add(x);
                        });
                    ctx.SaveChanges();
                }

                return items;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Contactos Update(Contactos item)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    item.DataAlteracao = DateTime.Now;
                    ctx.Contactos.Update(item);
                    ctx.SaveChanges();
                }

                return item;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static bool Delete(string id)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    Contactos contact = ctx.Contactos.Where(x => x.No == id).FirstOrDefault();
                    if (contact != null)
                    {
                        ctx.Contactos.Remove(contact);
                        ctx.SaveChanges();
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        public static bool Delete(Contactos item)
        {
            return Delete(new List<Contactos> { item });
        }

        public static bool Delete(List<Contactos> items)
        {
            try
            {
                using (var ctx = new SuchDBContext())
                {
                    ctx.Contactos.RemoveRange(items);
                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        #endregion

        public static ContactViewModel ParseToViewModel(this Contactos item)
        {
            if (item != null)
            {
                return new ContactViewModel()
                {
                    No = item.No,
                    NoCliente = item.NoCliente,
                    NoServico = item.NoServico,
                    NoFuncao = item.NoFuncao,
                    Nome = item.Nome,
                    Telefone = item.Telefone,
                    Telemovel = item.Telemovel,
                    Fax = item.Fax,
                    Email = item.Email,
                    Pessoa = item.Pessoa,
                    Notas = item.Notas,
                    CriadoPor = item.CriadoPor,
                    DataCriacaoText = item.DataCriacao.HasValue ? item.DataCriacao.Value.ToString("yyyy-MM-dd") : "",
                    AlteradoPor = item.AlteradoPor,
                    DataAlteracaoText = item.DataAlteracao.HasValue ? item.DataAlteracao.Value.ToString("yyyy-MM-dd") : ""
                };
            }
            return null;
        }

        public static List<ContactViewModel> ParseToViewModel(this List<Contactos> items)
        {
            List<ContactViewModel> contacts = new List<ContactViewModel>();
            if(items != null)
                items.ForEach(x =>
                    contacts.Add(x.ParseToViewModel()));
            return contacts;
        }

        public static Contactos ParseToDB(this ContactViewModel item)
        {
            if (item != null)
            {
                return new Contactos()
                {
                    No = item.No,
                    NoCliente = item.NoCliente,
                    NoServico = item.NoServico,
                    NoFuncao = item.NoFuncao,
                    Nome = item.Nome,
                    Telefone = item.Telefone,
                    Telemovel = item.Telemovel,
                    Fax = item.Fax,
                    Email = item.Email,
                    Pessoa = item.Pessoa,
                    Notas = item.Notas,
                    CriadoPor = item.CriadoPor,
                    DataCriacao = string.IsNullOrEmpty(item.DataCriacaoText) ? (DateTime?)null : DateTime.Parse(item.DataCriacaoText),
                    AlteradoPor = item.AlteradoPor,
                    DataAlteracao = string.IsNullOrEmpty(item.DataAlteracaoText) ? (DateTime?)null : DateTime.Parse(item.DataAlteracaoText)
                };
            }
            return null;
        }

        public static List<Contactos> ParseToDB(this List<ContactViewModel> items)
        {
            List<Contactos> contacts = new List<Contactos>();
            if (items != null)
                items.ForEach(x =>
                    contacts.Add(x.ParseToDB()));
            return contacts;
        }
    }
}
