using LimaArrendamentos.Data.Entities;
using LimaArrendamentos.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LimaArrendamentos.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IRealtyRepository _realtyRepository;
        private readonly ITypologyRepository _typologyRepository;
        private readonly IEnergyClassRepository _energyClassRepository;
        private readonly IPropertyTypeRepository _propertyTypeRepository;

        public SeedDb(
            DataContext context, 
            IUserHelper userHelper,
            IRealtyRepository realtyRepository,
            ITypologyRepository typologyRepository,
            IEnergyClassRepository energyClassRepository,
            IPropertyTypeRepository propertyTypeRepository)
        {
            _context = context;
            _userHelper = userHelper;
            _realtyRepository = realtyRepository;
            _typologyRepository = typologyRepository;
            _energyClassRepository = energyClassRepository;
            _propertyTypeRepository = propertyTypeRepository;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Cliente");
            await _userHelper.CheckRoleAsync("Staff");

            //ADMIN
            var user = await _userHelper.GetUserByEmailAsync("limarrendamentos@gmail.com");
            if (user == null)
            {

                
                user = new User
                {
                    FirstName = "Lima",
                    LastName = "Arrendamentos",
                    Email = "limarrendamentos@gmail.com",
                    UserName = "limarrendamentos@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "Rua Salmão Real 123 nº6"
                    //BrandId = _context.Models.FirstOrDefault().brands.FirstOrDefault().Id,
                    //Brand = _context.Models.FirstOrDefault().brands.FirstOrDefault()
                };

                

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }


            //CLIENTE
            var user2 = await _userHelper.GetUserByEmailAsync("cliente@limarrendamentos.com");
            if (user2 == null)
            {

                
                user2 = new User
                {
                    FirstName = "Catarina",
                    LastName = "Lima",
                    Email = "cliente@limarrendamentos.com",
                    UserName = "cliente@limarrendamentos.com",
                    PhoneNumber = "123456789",
                    Address = "Rua Salmão Real 123 nº6"
                    
                };


                var result = await _userHelper.AddUserAsync(user2, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user2, "Cliente");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user2);
                await _userHelper.ConfirmEmailAsync(user2, token);

            }


            //CLIENTE
            var user3 = await _userHelper.GetUserByEmailAsync("cliente2@limarrendamentos.com");
            if (user3 == null)
            {

                user3 = new User
                {
                    FirstName = "Joaquina",
                    LastName = "Francisca",
                    Email = "cliente2@limarrendamentos.com",
                    UserName = "cliente2@limarrendamentos.com",
                    PhoneNumber = "123456789",
                    Address = "Rua falsa 123"

                };


                var result = await _userHelper.AddUserAsync(user3, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user3, "Cliente");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user3);
                await _userHelper.ConfirmEmailAsync(user3, token);

            }

            //Staff
            var userstaff = await _userHelper.GetUserByEmailAsync("staff@limarrendamentos.com");
            if (userstaff == null)
            {

                userstaff = new User
                {
                    FirstName = "Francisco",
                    LastName = "António",
                    Email = "staff@limarrendamentos.com",
                    UserName = "staff@limarrendamentos.com",
                    PhoneNumber = "123456789",
                    Address = "Rua Salmão Real 123 nº6"
                    
                };



                var result = await _userHelper.AddUserAsync(userstaff, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(userstaff, "Staff");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(userstaff);
                await _userHelper.ConfirmEmailAsync(userstaff, token);

            }

            //Staff2
            var userstaff2 = await _userHelper.GetUserByEmailAsync("staff2@limarrendamentos.com");
            if (userstaff2 == null)
            {

                userstaff2 = new User
                {
                    FirstName = "Roberto",
                    LastName = "Amadeu",
                    Email = "staff2@limarrendamentos.com",
                    UserName = "staff2@limarrendamentos.com",
                    PhoneNumber = "123456789",
                    Address = "Rua Salmão Real 123 nº6"
                    //BrandId = _context.Models.FirstOrDefault().brands.FirstOrDefault().Id,
                    //Brand = _context.Models.FirstOrDefault().brands.FirstOrDefault()
                };



                var result = await _userHelper.AddUserAsync(userstaff2, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(userstaff2, "Staff");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(userstaff2);
                await _userHelper.ConfirmEmailAsync(userstaff2, token);

            }

            //TYPOLOGY
            if (!_context.Typologies.Any())
            {
                AddTypology("T0");
                AddTypology("T1");
                AddTypology("T2");
                AddTypology("T3");
                AddTypology("T4");
                AddTypology("T5");
                await _context.SaveChangesAsync();
            }

            //ENERGY CLASS
            if (!_context.EnergyClasses.Any())
            {
                AddEnergyClass("A+++");
                AddEnergyClass("A++");
                AddEnergyClass("A+");
                AddEnergyClass("A");
                AddEnergyClass("B");
                AddEnergyClass("C");
                AddEnergyClass("D");

                await _context.SaveChangesAsync();
            }

            ////REALTY TYPE
            if (!_context.PropertyTypes.Any())
            {
                AddPropertyType("Apartamento");
                AddPropertyType("Moradia");
                AddPropertyType("Herdade");
                AddPropertyType("Estúdio");
                AddPropertyType("Escritório");

                await _context.SaveChangesAsync();

            }


            //typology conversion
            var typologyt0 = await _typologyRepository.GetByIdAsync(1);
            var typologyt1 = await _typologyRepository.GetByIdAsync(2);
            var typologyt2 = await _typologyRepository.GetByIdAsync(3);
            var typologyt3 = await _typologyRepository.GetByIdAsync(4);
            var typologyt4 = await _typologyRepository.GetByIdAsync(5);
            var typologyt5 = await _typologyRepository.GetByIdAsync(6);

            //energyclass conversion
            var energyclassAmaismaismais = await _energyClassRepository.GetByIdAsync(6);
            var energyclassAmaismais = await _energyClassRepository.GetByIdAsync(5);
            var energyclassAmais = await _energyClassRepository.GetByIdAsync(4);
            var energyclassA = await _energyClassRepository.GetByIdAsync(3);
            var energyclassB = await _energyClassRepository.GetByIdAsync(2);
            var energyclassC = await _energyClassRepository.GetByIdAsync(1);
            var energyclassD = await _energyClassRepository.GetByIdAsync(7);

            //property type conversion
            var propertyTypeApartamento = await _propertyTypeRepository.GetByIdAsync(5);
            var propertyTypeMoradia = await _propertyTypeRepository.GetByIdAsync(4);
            var propertyTypeHerdade = await _propertyTypeRepository.GetByIdAsync(3);
            var propertyTypeEstudio = await _propertyTypeRepository.GetByIdAsync(2);
            var propertyTypeEscritorio = await _propertyTypeRepository.GetByIdAsync(1);


            if (!_context.Realties.Any())
            {
               
                _context.Realties.Add(new Realty
                {
                    Typology = typologyt0.TypologyDesc,
                    AnnouncementTitle = "Arrenda-se T0 em Carnaxide",
                    Description = "Excelente apartamento T0 em bom estado de conservação para arrendamento em Carnaxide, " +
                    "no Concelho de Oeiras, numa zona bastante tranquila e perto do Centro cívico, este fantástico Apartamento " +
                    "situa-se inserido num prédio de 7 andares é uma cave mas pode ser considerado como um r/c, com uma área útil de aproximadamente 50 m2.",
                    EnergyClass = energyclassA.EnergyClassDesc,
                    Address = "Rua dos poços",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 420,
                    Username = "cliente2@limarrendamentos.com",
                    PropertyType = propertyTypeApartamento.PropertyTypeDesc,
                    
                   
                });

                _context.Realties.Add(new Realty
                {
                    Typology = typologyt1.TypologyDesc,
                    AnnouncementTitle = "BENFICA (Av. G. Pereira) próx.Comboio e PSP- T1 Mobilado e equipado",
                    Description = "Apartamento T1  com cozinha, totalmente mobilado e equipado, bem situado, soalheiro, prédio com elevador.",
                    EnergyClass = energyclassB.EnergyClassDesc,
                    Address = "Rua do Poço Novo,21",
                    nBathrooms = 1,
                    nBedrooms = 2,
                    Value = 360,
                    Username = "cliente2@limarrendamentos.com",
                    PropertyType = propertyTypeApartamento.PropertyTypeDesc
                });

                _context.Realties.Add(new Realty
                {
                    Typology = typologyt1.TypologyDesc,
                    AnnouncementTitle = "São Marcos - Amplo apartamento T1 com arrecadação e bons acabamentos.",
                    Description = "Excelente apartamento T1 em prédio recente de São Marcos com áreas fabulosas, situado numa praceta muito sossegada e com facilidade de estacionamento.",
                    EnergyClass = energyclassD.EnergyClassDesc,
                    Address = "Avenida do Brasil, nº25A ",
                    nBathrooms = 1,
                    nBedrooms = 3,
                    Value = 400,
                    Username = "cliente2@limarrendamentos.com",
                    PropertyType = propertyTypeApartamento.PropertyTypeDesc
                });

                _context.Realties.Add(new Realty
                {
                    Typology = typologyt2.TypologyDesc,
                    AnnouncementTitle = "T2 c/ PÁTIO às Amoreiras/Campo de Ouriquea",
                    Description = "Arrendo T2 com cerca de 50 m2 + pátio exterior com 20 m2, em casa térrea situada na Travessa de Santo Aleixo, em Campo de Ourique, junto às Amoreiras.",
                    EnergyClass = energyclassAmaismaismais.EnergyClassDesc,
                    Address = "Av. Republica da bulgária",
                    nBathrooms = 1,
                    nBedrooms = 7,
                    Value = 650,
                    Username = "cliente2@limarrendamentos.com",
                    PropertyType = propertyTypeApartamento.PropertyTypeDesc
                });

                _context.Realties.Add(new Realty
                {
                    Typology = typologyt3.TypologyDesc,
                    AnnouncementTitle = "T3 perto do metro, Odivelas!",
                    Description = "Localizado em zona central e sossegada de Odivelas, perto de transportes públicos, serviços e comércio.",
                    EnergyClass = energyclassB.EnergyClassDesc,
                    Address = "Av. Republica da bulgária",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 900,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeApartamento.PropertyTypeDesc
                });

                _context.Realties.Add(new Realty
                {
                    Typology = typologyt4.TypologyDesc,
                    AnnouncementTitle = "T4 Cacém a 200metros Estação Mobilado",
                    Description = "Apartamento em 2º Andar com muita luminosidade (c/2 frentes) e elevador, muito espaçoso (85m2), equipado/mobilado.",
                    EnergyClass = energyclassC.EnergyClassDesc,
                    Address = "Largo D. Maria II ",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 800,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeApartamento.PropertyTypeDesc
                });

                _context.Realties.Add(new Realty
                {
                    Typology = typologyt5.TypologyDesc,
                    AnnouncementTitle = "Arrendamento T5 junto ao Areeiro",
                    Description = "Moradia localizada entre a rotunda do Areeiro e a estação de comboios Roma-Areeiro com uma envolvente bem servida de transportes (metro, comboios e autocarros), serviços e comércio.",
                    EnergyClass = energyclassAmais.EnergyClassDesc,
                    Address = "Rua dos Jerónimos",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 1500,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeMoradia.PropertyTypeDesc
                });

                _context.Realties.Add(new Realty
                {
                    Typology = typologyt1.TypologyDesc,
                    AnnouncementTitle = "T1 Marquês de Pombal",
                    Description = "Apartamento com excelente localização, no centro de Lisboa, junto a av. Fonte Pereira de Melo, a zona tem todos os serviços e vários transportes Carris, muito próximo das estações do metro de: Marques de POmbal, Parque, Picoas e São Sebastião, está a 10 minutos a pé de Corte Inglês, tem a volta os correios, restaurantes, cafés, lojas de vestir, farmacias, tem vários supermercados entre eles: MiniPreço, Pingo Doce.",
                    EnergyClass = energyclassAmaismais.EnergyClassDesc,
                    Address = "Rua dos Anjos 63",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 600,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeApartamento.PropertyTypeDesc
                });

                //////////////////////////////////////////////////////////
                _context.Realties.Add(new Realty
                {
                    Typology = typologyt1.TypologyDesc,
                    AnnouncementTitle = "Escritório em Carnaxide com 750 m2",
                    Description = "Escritório em Carnaxide para arrendamento, com 750 m2, que ocupa um piso inteiro, situado num conceituado condomínio empresarial, sede de várias empresas multinacionais em Portugal.",
                    EnergyClass = energyclassAmaismais.EnergyClassDesc,
                    Address = "Rua dos Anjos 63",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 2500,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeEscritorio.PropertyTypeDesc
                });
                _context.Realties.Add(new Realty
                {
                    Typology = typologyt3.TypologyDesc,
                    AnnouncementTitle = "Escritório- Torres Olivais II, Lisboa",
                    Description = "Escritório com boa localização, situado na II Fase do Spacio Shopping dos Olivais fica por cima do shopping.",
                    EnergyClass = energyclassB.EnergyClassDesc,
                    Address = "Av. Republica da bulgária",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 1500,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeEscritorio.PropertyTypeDesc
                });
                _context.Realties.Add(new Realty
                {
                    Typology = typologyt4.TypologyDesc,
                    AnnouncementTitle = "Moradia V3-Arrendamento Olivais",
                    Description = "Moradia com 4 assoalhadas em Lisboa na zona dos Olivais.",
                    EnergyClass = energyclassB.EnergyClassDesc,
                    Address = "Avenida Miguel Bombarda",
                    nBathrooms = 2,
                    nBedrooms = 4,
                    Value = 1400,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeMoradia.PropertyTypeDesc
                });
                _context.Realties.Add(new Realty
                {
                    Typology = typologyt3.TypologyDesc,
                    AnnouncementTitle = "Andar de Moradia T3 com jardim",
                    Description = "Andar de moradia totalmente recuperada e parcialmente mobilado para arrendamento.",
                    EnergyClass = energyclassB.EnergyClassDesc,
                    Address = "Av. Eng. Adelino Amaro da Costa",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 1000,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeMoradia.PropertyTypeDesc
                });
                _context.Realties.Add(new Realty
                {
                    Typology = typologyt0.TypologyDesc,
                    AnnouncementTitle = "Estúdio mobilado para arrendamento no Chiado, Lisboa",
                    Description = "Apartamento T0 mobilado, para arrendamento na Misericórdia, Lisboa. A poucos minutos do Museu das Comunicações e do ISEG - Instituto Superior de Economia e Gestão da Universidade de Lisboa.",
                    EnergyClass = energyclassB.EnergyClassDesc,
                    Address = "Rua Rodrigo da Fonseca",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 200,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeEstudio.PropertyTypeDesc
                });
                _context.Realties.Add(new Realty
                {
                    Typology = typologyt0.TypologyDesc,
                    AnnouncementTitle = "Apartamento T0 sem móveis localizado em prédio com piscina",
                    Description = "Apartamento T0 sem móveis localizado em prédio com piscina no Parque Europa",
                    EnergyClass = energyclassB.EnergyClassDesc,
                    Address = "Avenida Marginal 8648B",
                    nBathrooms = 4,
                    nBedrooms = 5,
                    Value = 300,
                    Username = "cliente@limarrendamentos.com",
                    PropertyType = propertyTypeEstudio.PropertyTypeDesc
                });




                await _context.SaveChangesAsync();
            }

        }

        private void AddTypology(string desc)
        {
            _context.Typologies.Add(new Typology
            {
                TypologyDesc = desc
            });
        }

        private void AddEnergyClass(string desc)
        {
            _context.EnergyClasses.Add(new EnergyClass
            {
                EnergyClassDesc = desc
            });
        }

        private void AddPropertyType(string desc)
        {
            _context.PropertyTypes.Add(new PropertyType
            {
                PropertyTypeDesc = desc
            });
        }

        //private void AddRealty(Typology typology, string title, string description, EnergyClass energyclass, string address, int bathrooms, int bedrooms, int value, DateTime constructionyear, PropertyType propertyType)
        //{
        //    _context.Realties.Add(new Realty
        //    {
        //       Typology = typology.ToString(),
        //       AnnouncementTitle = title,
        //       Description = description,
        //       EnergyClass = energyclass.ToString(),
        //       Address = address,
        //       nBathrooms = bathrooms,
        //       nBedrooms = bedrooms,
        //       Value = value,
        //       ConstructionYear = constructionyear,
        //       PropertyType = propertyType.ToString()


        //    });
        //}

    }
}
