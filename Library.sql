create database Library_db
drop database Library_db
use Library_db

update produit set qte =1000
create table produit(
id int primary key identity,
nom nvarchar(200),
categorie varchar(200),
prix float,
matiere nvarchar(200),
Nscolaire nvarchar(200),
qte int,
valeur int,
emplacement varchar(50),
imagedata image,
description varchar(300),
codebar varchar(50) unique,
remise float,
codebarStatus varchar(10) check (codebarStatus in ('scanned','generated')))

create table Fournisseur (id int  primary key identity,
nom varchar(50),
email varchar(50),
adresse varchar(150),
tel varchar(15))

create table Bon_Livraison(
Nentre int primary key identity,
NBon int,
fournisseur int foreign key references fournisseur(id) ,
dateentre date,
Montant_Ht float,
montant_Tva float,
montant_TTC float,
Verification varchar(15),
CommandeId varchar(20) default 'NULL')

create Trigger trg1 on Bon_Livraison for insert as 
begin
 select Nentre from inserted

end


create table produit_Bon_Livraison(
facture_id int foreign key references Bon_Livraison(Nentre) not null,
produit_id int foreign key references produit(id) not null,
Unite Varchar(20),
QteEntre int,
prixunitaire float,
remise float,
tva float)
alter table produit_Bon_Livraison
add constraint pk1 primary key(facture_id,produit_id)




create table client(CIN varchar(15) primary key not null,
nom varchar(20),
prenom varchar(20),
tel varchar(12),
email varchar(50),
adresse varchar(50))
insert into Client Values('1','Normal Client','','','','')


Select cin as 'C.I.N',(nom+' '+prenom) as 'Nom',tel as 'Tel',email as 'Email',adresse as 'Adresse' from client

create table Bill(id int primary key identity not null,
client_CIN varchar(15) foreign key references client(CIN),
datesortie date,
montant float,
Status varchar(50),
montantRest float)

create Trigger trg2 on Bill for insert
as 
begin
 select id from inserted

end


create table Bill_products(
product_id int foreign key references produit(id) not null,
Bill_id int foreign key references Bill(id) not null,
Unite varchar(20),
quantite int,
remise int)
alter table Bill_products
add constraint pk2 primary key(product_id,Bill_id)

insert into Fournisseur values 
('DAR SOULAMIAL HADITA','','',''),
('DAR IHYA EL OULOUM','','',''),
('DAR AL MASSAR','','',''),
('MEDIPAF','','',''),
('STAR SCHOOL','','',''),
('BOUBKAR','','',''),
('EDISOFT','','',''),
('ETABLISSEMENT ARRISSALA','','',''),
('FOX DISTRY','','',''),
('MAISON INTERNATIONAL DU LIVRE','','',''),
('IMARSI','','',''),
('LIBRAIRIE PAPETERIE NATIONALE','','',''),
('EDITION EL ATLASSI','','',''),
('EDITION MOUAD','','',''),
('DEVINK','','',''),
('NRG','','',''),
('LIBRAIRIE ESSALAM AL JADIDA','','',''),
('O.M.A.B','','',''),
('SICLA','','',''),
('GSB GROUPE SALHI BUSINESS','','',''),
('Ets LA RENAISSANCE SARL EDITION ET DISTRIBUTION','','',''),
('TOP BLEU BLEU','','',''),
('DAR AL OUMA SARL EDITION ET DIFFUSION','','',''),
('PAPETERIE LALLA GHITA','','',''),
('MARACI','','',''),
('TOVEL SARL','','',''),
('LIB DAR CHOUROUK','','','')

create table commande(id int identity primary key not null,
Fournisseur_id int foreign key references fournisseur(id),
datecommande date,
status varchar(25))
create Trigger trg3 on commande for insert
as 
begin
 select id from inserted

end

create table produit_commande(
product_id int foreign key references produit(id) not null,
commande_id int foreign key references commande(id) not null,
unite varchar(20),
quantiteDemande int,
quantiteLivrée int,
quantiteReste int)

alter table produit_commande
add constraint pk3 primary key(product_id,commande_id)

Create table Users(id int identity ,
username Varchar(50),
password Varchar(50),
nom_complet varchar(50),
email varchar(50),
account_Type varchar(50))

insert into Users values('kin1','0123','hatim rachid','hat-124@hotmail.fr','ADMIN')

create table marche(Nmarche varchar(60) primary key,
nomMarch varchar(50),
Dudate date,
addDate date,
client varchar(50),
adresse varchar(50),
objet varchar(1500))

create table marcheProduit(id int identity primary key,
Nmarche varchar(60) foreign key references marche(Nmarche),
designation nvarchar(150),
unite varchar(20),
quantite int,
prix float,
tva int)


--archive
delete from produit_Bon_LivraisonAR
delete from Bon_LivraisonAR
delete from Bill_productsAR
delete from BillAR
delete from produit_commandeAR
delete from commandeAR
select * from BillAR

drop table commandeAR
--archive Query -----------------------------------------------------
insert into Bon_LivraisonAR select Nentre,NBon,fournisseur,dateentre,Montant_Ht,montant_Tva,montant_TTC,Verification,CommandeId from Bon_Livraison where Verification like 'Verifié'
insert into produit_Bon_LivraisonAR select * from produit_Bon_Livraison where produit_Bon_Livraison.facture_id in (select facture_id from Bon_Livraison where Verification like 'Verifié')

insert into BillAR select id,client_CIN,datesortie,montant,Status,montantRest from Bill where Bill.Status='Payé'
insert into Bill_productsAR select * from Bill_products where Bill_id in (select Bill_id from Bill where Bill.Status='Payé')

insert into commandeAR select id,Fournisseur_id,datecommande,status from commande where status='Livré'
insert into produit_commandeAR select * from produit_commande where produit_commande.commande_id in (select id from commande where status='Livré')


DBCC CHECKIDENT ('Bon_Livraison', RESEED, 1)

delete from Bill_products
delete from Bill

DBCC CHECKIDENT ('Bill', RESEED, 1)

delete from produit_commande
delete from commande

DBCC CHECKIDENT ('commande', RESEED, 1)
--------------------------------------------------------------------
update produit set prix=1 where prix is null


drop table commandeAR


create table Bon_LivraisonAR(
Nentre int primary key,
NBon int,
fournisseur int foreign key references fournisseur(id) ,
dateentre date,
Montant_Ht float,
montant_Tva float,
montant_TTC float,
Verification varchar(15),
CommandeId varchar(20) default 'NULL')


create Trigger trg4 on Bon_LivraisonAR for insert as 
begin
 select Nentre from inserted

end


create table produit_Bon_LivraisonAR(
facture_id int foreign key references Bon_LivraisonAR(Nentre) not null,
produit_id int foreign key references produit(id) not null,
Unite Varchar(20),
QteEntre int,
prixunitaire float,
remise float,
tva float)
alter table produit_Bon_LivraisonAR
add constraint pk4 primary key(facture_id,produit_id)


create table BillAR(id int primary key  not null,
client_CIN varchar(15) foreign key references client(CIN),
datesortie date,
montant float,
Status varchar(50),
montantRest float)

create Trigger trg5 on BillAR for insert
as 
begin
 select id from inserted

end


create table Bill_productsAR(
product_id int foreign key references produit(id) not null,
Bill_id int foreign key references BillAR(id) not null,
Unite varchar(20),
quantite int,
remise int)
alter table Bill_productsAR
add constraint pk5 primary key(product_id,Bill_id)

create table commandeAR(id int  primary key not null,
Fournisseur_id int foreign key references fournisseur(id),
datecommande date,
status varchar(25))
create Trigger trg6 on commandeAR for insert
as 
begin
 select id from inserted

end

create table produit_commandeAR(
product_id int foreign key references produit(id) not null,
commande_id int foreign key references commandeAR(id) not null,
unite varchar(20),
quantiteDemande int,
quantiteLivrée int,
quantiteReste int)

alter table produit_commandeAR
add constraint pk6 primary key(product_id,commande_id)

----------------------------------------------------------log----------------------------------------------
create table DB_LOG(
log_id int,
log_description varchar(50),
log_type varchar(30),
log_date date,
log_time varchar(10),
log_reference_id varchar(15))


drop table DB_LOG



select * from BillAR

delete from Bon_LivraisonAR

select top 1 Nscolaire from  produit
                          inner join Bill_products on product_id = id
                          group by Nscolaire
                          order by count(*) desc

select id,nom,categorie,matiere,Nscolaire,prix,qte,valeur from produit+

select COUNT(*) from Bill where datesortie=CONVERT(date, getdate());
select CONVERT(date, getdate()),* from Bill
select sum(Bill.montant) from Bill where Bill.datesortie=CONVERT(date, getdate())

insert into produit select Désignation,Catégorie,Prix,Matiére,Niveau_Scolaire,qte,valuer,eplacement,NULL,description,codebar,10,'generated' from ImportedProducts
select * from ImportedProducts
select * from produit
update produit set prix=50 where prix is null