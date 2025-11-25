BEGIN TRANSACTION;

-- Start clean
DROP TABLE IF EXISTS Player;
DROP TABLE IF EXISTS Team;
DROP TABLE IF EXISTS Division;
DROP TABLE IF EXISTS BowlingUser;

CREATE TABLE BowlingUser (
    Id INTEGER PRIMARY KEY,
    UserName TEXT NOT NULL,
    PasswordHash TEXT NOT NULL,
    IsAdmin INTEGER NOT NULL CHECK (IsAdmin IN (0,1))
);

CREATE TABLE Division (
    DivisionId INTEGER PRIMARY KEY,
    DivisionName TEXT NOT NULL UNIQUE
);

CREATE TABLE Team (
    TeamId INTEGER PRIMARY KEY,
    TeamName TEXT NOT NULL,
    DivisionId INTEGER NOT NULL,
    RegistrationPaid INTEGER NOT NULL CHECK (RegistrationPaid IN (0,1)),
    PaymentDate TEXT,
    FOREIGN KEY (DivisionId) REFERENCES Division(DivisionId),
    CHECK (
        (RegistrationPaid = 1 AND PaymentDate IS NOT NULL) OR
        (RegistrationPaid = 0 AND PaymentDate IS NULL)
    )
);
CREATE TABLE Player (
    PlayerId INTEGER PRIMARY KEY,
    TeamId INTEGER NOT NULL,
    PlayerName TEXT NOT NULL,
    City TEXT NOT NULL,
    Province TEXT NOT NULL,
    Email TEXT NOT NULL,
    Phone TEXT NOT NULL,
    FOREIGN KEY (TeamId) REFERENCES Team(TeamId)
);

-- BowlingUser records
INSERT into BowlingUser values (1, 'smonk', 'AQAAAAIAAYagAAAAEIqCPZxk8RTs3R6K/dYZAWlDgCr83d3U3MDums/YuZEQl/gXqc+e7AsNVHp3jMBtOg==', 1);

-- Division records
INSERT INTO Division VALUES (1, 'Men''s');
INSERT INTO Division VALUES (2, 'Women''s');
INSERT INTO Division VALUES (3, 'Mixed');
INSERT INTO Division VALUES (4, 'Youth');
INSERT INTO Division VALUES (5, 'Senior');

-- Team records
insert into Team values (1, 'Holy Rollers', 4, 1, '2025-11-10');
insert into Team values (2, 'Jesus Take the Ball', 1, 1, '2025-12-03');
insert into Team values (3, 'Spare Disciples', 2, 1, '2025-12-04');
insert into Team values (4, 'Our Father Who Art in the Gutter', 1, 0, NULL);
insert into Team values (5, 'Let There Be Strikes', 4, 1, '2025-12-06');
insert into Team values (6, 'Split Happens', 1, 0, NULL);
insert into Team values (7, 'Strike Force', 3, 0, NULL);
insert into Team values (8, 'Spare Me', 2, 1, '2025-11-06');
insert into Team values (9, 'The Bowling Stones', 1, 1, '2025-11-01');
insert into Team values (10, 'The Lucky Strikes', 3, 0, NULL);
insert into Team values (11, 'Avengers', 5, 0, NULL);
insert into Team values (12, 'Justice League', 5, 1, '2025-10-22');
insert into Team values (13, 'Joestars', 4, 0, NULL);
insert into Team values (14, 'The Empire', 1, 1, '1977-05-25');
insert into Team values (15, 'The Seven', 5, 0, NULL);
insert into Team values (16, 'The Pinheads', 3, 1, '2025-11-07');
insert into Team values (17, 'Guardians of the Gutter', 1, 0, NULL);
insert into Team values (18, 'Slippin Jimmies', 2, 1, '2025-11-01');
insert into Team values (19, 'Appolosa', 5, 0, NULL);
insert into Team values (20, 'Freedom''s Finest', 3, 1, '2025-11-05');
insert into Team values (21, 'Pin Harmony', 2, 1, '2025-11-04');
insert into Team values (22, 'Alley Cats', 4, 0, NULL);
insert into Team values (23, 'Bowl Movements', 1, 0, NULL);
insert into Team values (24, 'All Too Spare', 3, 1, '2025-11-10');
insert into Team values (25, 'Folklane', 2, 1, '2025-11-02');
insert into Team values (26, 'Gutter Guys', 1, 0, NULL);
insert into Team values (27, 'Bumper Boys', 4, 0, NULL);
insert into Team values (28, 'Ford Pintos', 1, 1, '2025-11-02');
insert into Team values (29, 'Spare Nine', 2, 1, '2025-11-01');
insert into Team values (30, 'Turkey Dinner', 3, 1, '2025-11-04');
insert into Team values (31, 'The Mismatched Socks', 2, 0, NULL);
insert into Team values (32, 'The Strikers', 5, 0, NULL);
insert into Team values (33, 'Burly Bros', 1, 1, '2025-11-01');
insert into Team values (34, 'Rampant Ragers', 3, 1, '2025-11-02');
insert into Team values (35, 'Peak Performers', 4, 1, '2025-11-05');
insert into Team values (36, 'The Big Lebowski''s', 5, 1, '1998-03-06');
insert into Team values (37, 'Pin Pals', 1, 0, NULL);
insert into Team values (38, 'The Government', 5, 0, NULL);
insert into Team values (39, 'Moms Against Drunk Bowling', 2, 1, '2025-11-10');
insert into Team values (40, 'I Can''t Believe It''s Not Gutter', 1, 1, '2025-10-17');
insert into Team values (41, 'Kalahari Kings', 1, 0, NULL);
insert into Team values (42, 'Zambezi Rollers', 4, 1, '2025-11-05');
insert into Team values (43, 'Ubuntu United', 3, 0, NULL);
insert into Team values (44, 'Sahara Sweepers', 2, 1, '2025-11-15');
insert into Team values (45, 'Thunder Ngoma', 1, 1, '2025-10-30');
insert into Team values (46, 'Magic Mohawks', 4, 0, NULL);
insert into Team values (47, 'The Chimichangas', 3, 1, '2025-11-08');
insert into Team values (48, 'BigBros Burritos', 1, 1, '2025-09-17');
insert into Team values (49, 'Single Mommies Power', 2, 1, '2025-10-31');
insert into Team values (50, 'Lil Bro''s Childcare', 4, 1, '2025-11-06');


-- Player records
insert into Player values (1, 1, 'Andrew BowlsAlot', 'Calgary', 'MB', 'andrewbowls@gmail.com', '506-199-1123');
insert into Player values (2, 1, 'Larry BowlSumTime', 'Toronto', 'ON', 'larry.bowls@gmail.com', '506-223-8733');
insert into Player values (3, 1, 'Jerry Bowloften', 'Ottawa', 'ON', 'jerry.bowls@gmail.com', '506-321-1121');
insert into Player values (4, 1, 'Jimmy NevaBowl', 'Ottawa', 'ON', 'jimmy.bowls@gmail.com', '506-099-1102');
insert into Player values (5, 2, 'McLovin', 'Saint John', 'NB', 'McLovin@gmail.com', '506-430-9320');
insert into Player values (6, 2, 'Judy BowlOnce', 'Regina', 'SK', 'JudyBowl@hotmail.com', '506-688-3900');
insert into Player values (7, 2, 'Elijah McBowl', 'Halifax', 'NS', 'elijah.mcbowl@gmail.com', '506-211-9939');
insert into Player values (8, 2, 'Noah mcStrike', 'Saint John', 'NB', 'noah.mcstrike@gmail.com', '506-112-7654');
insert into Player values (9, 3, 'Pierre Le Pin', 'Truro', 'NS', 'pierre.lepin@gmail.com', '506-910-8211');
insert into Player values (10, 3, 'Olivier Pin-tois', 'Wolfville', 'NS', 'olivier.pintois@gmail.com', '506-918-6622');
insert into Player values (11, 3, 'Claude le Guttereur', 'Woodstock', 'NB', 'claude.guttereur@gmail.com', '321-500-9921');
insert into Player values (12, 3, 'David de la Split', 'Saint John', 'NB', 'david.split@gmail.com', '322-493-1992');
insert into Player values (13, 4, 'Jacques Sparereau', 'Rothesay', 'NB', 'jacques.sparereau@gmail.com', '506-882-9011');
insert into Player values (14, 4, 'Rene Deux-Pins', 'Quebec CIty', 'QC', 'rene.deuxpins@gmail.com', '506-221-8811');
insert into Player values (15, 4, 'Yves Split-iere', 'Montreal', 'QC', 'yves.splitiere@gmail.com', '506-777-7727');
insert into Player values (16, 4, 'Jacques Le Holy Roller', 'Saint John', 'NB', 'jacques.holyroller@gmail.com', '506-221-2002');
insert into Player values (17, 5, 'Jean-Paul Pinsson', 'Montreal', 'QC', 'jeanpaul.pinsson@gmail.com', '506-110-2084');
insert into Player values (18, 5, 'Gerard Brie-son', 'Montreal', 'QC', 'gerard.brieson@gmail.com', '506-124-8867');
insert into Player values (19, 5, 'Jimmy Spares', 'Saint John', 'NB', 'jimmy.spares@gmail.com', '506-676-7676');
insert into Player values (20, 5, 'Liam Strikes', 'Saint John', 'NB', 'liam.strikes@gmail.com', '506-888-8828');
insert into Player values (21, 6, 'Pinhead Larry', 'Bikini Bottom', 'OC', 'pinheadlarry@gmail.com', '506-922-1597');
insert into Player values (22, 6, 'Morgan Brown', 'Saint John', 'NB', 'morgan.brown180@outlook.com', '506-324-2451');
insert into Player values (23, 6, 'Devon Wilson', 'Oromocto', 'NB', 'devon.wilson477@gmail.com', '506-288-7026');
insert into Player values (24, 6, 'Jamie Brown', 'Fredericton', 'NB', 'jamie.brown700@gmail.com', '506-226-1284');
insert into Player values (25, 7, 'Riley Brown', 'Oromocto', 'NB', 'riley.brown651@hotmail.com', '506-999-6650');
insert into Player values (26, 7, 'Alex Thomas', 'Moncton', 'NB', 'alex.thomas901@outlook.com', '506-459-3363');
insert into Player values (27, 7, 'Morgan Jones', 'Saint John', 'NB', 'morgan.jones379@outlook.com', '506-689-1550');
insert into Player values (28, 7, 'Riley Thomas', 'Bathurst', 'NB', 'thomas276@hotmail.com', '506-432-6663');
insert into Player values (29, 8, 'Casey Moore', 'Campbellton', 'NB', 'casey.more372@gmail.com', '506-271-1532');
insert into Player values (30, 8, 'Avery Miller', 'Quispamsis', 'NB', 'avery.miller445@gmail.com', '506-326-4642');
insert into Player values (31, 8, 'Shawn Taylor', 'Elgin', 'NB', 'shawn.taylor331@outlook.com', '506-411-4444');
insert into Player values (32, 8, 'Chris Wilson', 'Hampton', 'NB', 'chris.wilson643@hotmail.com', '506-111-2233');
insert into Player values (33, 9, 'Sam Jones', 'Fredericton', 'NB', 'sam.jones281@gmail.com', '506-789-4403');
insert into Player values (34, 9, 'Chris Breezy', 'Saint John', 'NB', 'chris.breezy461@outlook.com', '506-333-0101');
insert into Player values (35, 9, 'Shawn Jackson', 'Sackville', 'NB', 'shawn.jackson910@outlook.com', '506-721-3392');
insert into Player values (36, 9, 'Jessie Davis', 'Salisbury', 'NB', 'jessie.davis641@gmail.com', '506-364-1111');
insert into Player values (37, 10, 'Casey Harris', 'Bathurst', 'NB', 'casey.harris137@outlook.com', '506-519-7192');
insert into Player values (38, 10, 'Taylor Brown', 'Dieppe', 'NB', 'taylor.brown882@hotmail.com', '506-841-9563');
insert into Player values (39, 10, 'Riley Thomas', 'Edmundston', 'NB', 'riley.thomas177@hotmail.com', '506-307-2421');
insert into Player values (40, 10, 'Sam Moore', 'Saint John', 'NB', 'sam.moore909@outlook.com', '506-478-2084');
insert into Player values (41, 11, 'Harrison Ford', 'Jackson', 'WY', 'hford@gmail.com', '506-223-1212');
insert into Player values (42, 11, 'Mark Hamill', 'Los Angeles', 'CA', 'markhamill@gmail.com', '506-222-1555');
insert into Player values (43, 11, 'Carrie Fisher', 'Alderaan', 'IR', 'rebelleader@star.com', '506-144-1224');
insert into Player values (44, 11, 'Darth Vader', 'Death Star', 'MR', 'anakinskywalker@jedi.com', '506-145-2515');
insert into Player values (45, 12, 'Sheev Palpatine', 'Death Star', 'MR', 'notasithlord@evil.com', '506-066-0066');
insert into Player values (46, 12, 'Yoda', 'Jedi Temple', 'CT', 'oldasheck@gmail.com', '506-102-1557');
insert into Player values (47, 12, 'Ryan Gosling', 'Cornwall', 'ON', 'ryangosling@gmail.com', '506-126-8887');
insert into Player values (48, 12, 'Leslie Nielson', 'Regina', 'SK', 'lnielson@gmail.com', '506-561-7260');
insert into Player values (49, 13, 'Joseph Joestar', 'New York City', 'NY', 'newyorkcityslacker@gmail.com', '506-111-1552');
insert into Player values (50, 13, 'Josuke Higashikata', 'Morioh', 'MP', 'ragingpanda103@gmail.com', '506-535-1958');
insert into Player values (51, 13, 'Giorno Giovanna', 'Naples', 'CP', 'giogio1985@gmail.com', '506-195-2055');
insert into Player values (52, 13, 'Jolyne Cujoh', 'Tampa', 'FL', 'greendolphincontact@hotmail.com', '506-129-4044');
insert into Player values (53, 14, 'Bruce Wayne', 'Gotham City', 'IL', 'notbatman@gmail.com', '506-124-3535');
insert into Player values (54, 14, 'Clark Kent', 'Metropolis', 'IL', 'notsuperman@gmail.com', '506-121-1556');
insert into Player values (55, 14, 'Mr Boss', 'Miramichi', 'NB', 'smilingfriends@gmail.com', '506-151-6622');
insert into Player values (56, 14, 'Bizarro Flash', 'Earth', 'PE', 'bizarro@gmail.com', '506-999-9999');
insert into Player values (57, 15, 'Homelander', 'New York City', 'NY', 'homelander@vought.com', '202-395-3080');
insert into Player values (58, 15, 'A-Train', 'New York City', 'NY', 'atrain@vought.com', '202-395-3080');
insert into Player values (59, 15, 'The Deep', 'New York City', 'NY', 'deep@vought.com', '202-395-3080');
insert into Player values (60, 15, 'Black Noir', 'New York City', 'NY', 'blacknoir@vought.com', '202-395-3080');
insert into Player values (61, 16, 'Jimmy Pinhead', 'Kelowna', 'BC', 'jimmy.pin@gmail.com', '250-112-3456');
insert into Player values (62, 16, 'Suzie Pinhead', 'Kelowna', 'BC', 'spinhead01@hotmail.com', '250-910-0329');
insert into Player values (63, 16, 'Dave Pinhead', 'Kelowna', 'BC', 'dave.pinhead@gmail.com', '250-239-2839');
insert into Player values (64, 16, 'Melanie Pinhead', 'Kelowna', 'BC', 'melpinhead111@outlook.com', '250-678-0000');
insert into Player values (65, 17, 'Rex Splode', 'Chicago', 'IL', 'mantzoukas@globeguards.com', '312-999-9932');
insert into Player values (66, 17, 'Zandale Randolph', 'Chicago', 'IL', 'pharoah@globeguards.com', '773-003-3293');
insert into Player values (67, 17, 'Rus Livingston', 'Underground Martian City', 'MA', 'schwartz@globeguards.com', '773-030-2020');
insert into Player values (68, 17, 'Rudolph Conners', 'Chicago', 'IL', 'marquand@globeguards.com', '773-123-4567');
insert into Player values (69, 18, 'Jimmy McGill', 'Cicero', 'IL', 'jimmy.mcgill@outlook.com', '643-562-6418');
insert into Player values (70, 18, 'Mike Ehrmantraut', 'Philadelphia', 'PA', 'mike.ehr@outlook.com', '255-269-1611');
insert into Player values (71, 18, 'Lydia Rodarte-Quayle', 'Glasgow', 'SL', 'lydia.rodarte@outlook.com', '255-754-1247');
insert into Player values (72, 18, 'Kim Wexler', 'Red Cloud', 'NB', 'kim.wexler@outlook.com', '255-842-2484');
insert into Player values (73, 19, 'Ed Harris', 'Atlantic City', 'NJ', 'ed.harris14@outlook.com', '882-982-3578');
insert into Player values (74, 19, 'Viggo Mortensen', 'Watertown', 'NU', 'viggo.mortensen67@outlook.com', '882-294-5964');
insert into Player values (75, 19, 'Jeremy Irons', 'Cowes', 'QC', 'jeremy.ironss@outlook.com', '882-674-2478');
insert into Player values (76, 19, 'Lance Henricksen', 'Manhattan', 'YT', 'lance.hens14@hotmail.com', '656-358-3739');
insert into Player values (77, 20, 'John Helldiver', 'Gun', 'AB', 'john.helldiv4@hotmail.com', '656-557-3268');
insert into Player values (78, 20, 'Coretta Kelly', 'Port Mercy', 'MB', 'halo.enjoyer@hotmail.com', '656-136-4854');
insert into Player values (79, 20, 'General Brasch', 'No City is Worthy', 'SE', 'brash.gen143@hotmail.com', '345-234-9341');
insert into Player values (80, 20, 'Shipmaster Ingaki', 'St. Mary', 'NS', 'shipper.inaki@hotmail.com', '656-439-3523');
insert into Player values (81, 21, 'Brittany Wilcox', 'Saint John', 'NB', 'bwilcox@gmail.com', '506-798-7896');
insert into Player values (82, 21, 'Brooke Wilson', 'Saint John', 'NB', 'bwilson@gmail.com', '506-996-9865');
insert into Player values (83, 21, 'Tammy Wilcox', 'Saint John', 'NB', 'twilcox@live.ca', '708-896-7896');
insert into Player values (84, 21, 'Morgan Wilson', 'Saint John', 'NB', 'mwilson@icloud.ca', '489-888-9632');
insert into Player values (85, 22, 'Bella Swan', 'Forks', 'WA', 'loca@gmail.com', '519-889-9631');
insert into Player values (86, 22, 'Edward Cullen', 'Forks', 'WA', 'eddy@live.ca', '506-789-9635');
insert into Player values (87, 22, 'Charlie Swan', 'Forks', 'WA', 'thechief@forkspd.ca', '569-999-7896');
insert into Player values (88, 22, 'Rosalie Cullen', 'Forks', 'WA', 'rose@gmail.com', '456-896-7899');
insert into Player values (89, 23, 'Bob Green', 'Halifax', 'NS', 'bgreen@hotmail.com', '459-888-7896');
insert into Player values (90, 23, 'Tom Hanks', 'Halifax', 'NS', 'tommy@gmail.com', '456-963-7412');
insert into Player values (91, 23, 'Timmy Turner', 'Halifax', 'NS', 'tt@gmail.com', '963-999-7896');
insert into Player values (92, 23, 'Bill Clinton', 'Halifax', 'NS', 'billyc@icloud.com', '506-666-3697');
insert into Player values (93, 24, 'Travic Kelce', 'Toronto', 'ON', 'travyy@swifty.com', '456-896-7896');
insert into Player values (94, 24, 'Pat Mcgrath', 'Toronto', 'ON', 'pattypat@gmail.com', '456-789-8523');
insert into Player values (95, 24, 'Selena Gomez', 'Toronto', 'ON', 'selenerrare@rare.com', '546-789-4123');
insert into Player values (96, 24, 'Taylor Swift', 'Toronto', 'ON', 'ttpd@ttpd.ca', '698-896-8745');
insert into Player values (97, 25, 'Betty Jones', 'Sussex', 'NB', 'bettyj@gmail.com', '506-654-7753');
insert into Player values (98, 25, 'Willow Smith', 'Sussex', 'NB', 'willows@gmail.com', '506-789-8896');
insert into Player values (99, 25, 'Daisy Mae', 'Sussex', 'NB', 'daisy@hotmail.com', '506-789-5555');
insert into Player values (100, 25, 'Ivy Wilde', 'Sussex', 'NB', 'ivyw@live.com', '506-789-7777');
insert into Player values (101, 26, 'Jenson Alibaba', 'Saint John', 'NB', 'alilbaba@gmail.com', '506-647-1112');
insert into Player values (102, 26, 'Abraxas Jones', 'Rothesay', 'NB', 'ajones@gmail.com', '506-799-0420');
insert into Player values (103, 26, 'Michael McQueen', 'Sussex', 'NB', 'mikemcqueen@hotmail.com', '506-849-2992');
insert into Player values (104, 26, 'Adam Scott', 'Gimli', 'MB', 'scottadam@gmail.com', '506-888-2944');
insert into Player values (105, 27, 'Noah Linten', 'Emo', 'ON', 'noahlinten@gmail.com', '506-849-5575');
insert into Player values (106, 27, 'Charlie Swath', 'Kimberley', 'BC', 'swathcharlie@outlook.com', '506-849-2295');
insert into Player values (107, 27, 'Cameron Barbos', 'Radville', 'SK', 'cambarb@gmail.com', '506-849-7792');
insert into Player values (108, 27, 'Albert Mahomes', 'Alberton', 'PE', 'albertm@gmail.com', '506-849-9525');
insert into Player values (109, 28, 'Michael Scott', 'Scotstown', 'QC', 'scottstots@outlook.com', '506-849-9742');
insert into Player values (110, 28, 'Dwight Schrute', 'St. Andrews', 'NB', 'dwightschrute@gmail.com', '506-777-8929');
insert into Player values (111, 28, 'Jim Halpert', 'Jimtown', 'NS', 'halpertjim@hotmail.com', '506-792-4489');
insert into Player values (112, 28, 'Stanley Hudson', 'Stanley', 'NB', 'shudson@gmail.com', '506-647-0113');
insert into Player values (113, 29, 'Jessica Alba', 'Philadelphia', 'PA', 'jesalb@outlook.com', '719-909-1234');
insert into Player values (114, 29, 'Vanessa Kirby', 'New York', 'NY', 'vkirbyy@hotmail.com', '506-721-6688');
insert into Player values (115, 29, 'Britt Lower', 'Harvey', 'NB', 'helly@gmail.com', '506-886-9087');
insert into Player values (116, 29, 'Hailey Steinfield', 'St. George', 'NB', 'hsfield@outlook.com', '506-987-1235');
insert into Player values (117, 30, 'Anna Gunn', 'Albuqerque', 'NM', 'AG@hotmail.com', '506-514-1227');
insert into Player values (118, 30, 'Bryan Cranston', 'Albuqerque', 'NM', 'waltwhite@gmail.com', '506-514-1337');
insert into Player values (119, 30, 'Arnold Wilcox', 'St. Stephen', 'NB', 'arnywilcox@outlook.com', '506-754-1679');
insert into Player values (120, 30, 'Stephen Gray', 'St. Andrews', 'NB', 'sg@hotmail.com', '506-222-1215');
insert into Player values (121, 31, 'John Michaels', 'Saint John', 'NB', 'jmichaels@gmail.com', '506-456-7847');
insert into Player values (122, 31, 'Raymond Dubly', 'Rosthey', 'NB', 'rayray320@hotmail.com', '506-897-1111');
insert into Player values (123, 31, 'Gordon Tyme', 'Charlottetown', 'PE', 'itsgordtyme@hotmail.com', '902-589-7742');
insert into Player values (124, 31, 'Liam Wilkins', 'Halifax', 'NS', 'liam.wilkins@gmail.com', '902-774-6348');
insert into Player values (125, 32, 'Trent Riply', 'New Glassgow', 'NS', 'trentthegent@hotmail.com', '782-348-8721');
insert into Player values (126, 32, 'Ryan Franks', 'Fredericton', 'NB', 'ryanfranks442@outlook.com', '506-997-4167');
insert into Player values (127, 32, 'Timothy Richards', 'Moncton', 'NB', 'timthetinman2@yahoo.com', '506-331-7441');
insert into Player values (128, 32, 'Velma Hubbards', 'Quebec City', 'QC', 'velma.hubbards@yahoo.com', '418-884-9964');
insert into Player values (129, 33, 'Rick Johnson', 'Miramichi', 'NB', 'rickyj99@hotmail.com', '506-452-8742');
insert into Player values (130, 33, 'Donald Roberts', 'Saint John', 'NB', 'd.roberts86@gmail.com', '506-333-4716');
insert into Player values (131, 33, 'Robert Haynes', 'Quispamsis', 'NB', 'haynesr333@gmail.com', '506-788-1547');
insert into Player values (132, 33, 'Kayla Sykes', 'Summerside', 'PE', 'ksykes22@hotmail.com', '902-555-8795');
insert into Player values (133, 34, 'Thomas Flownder', 'Saint John', 'NB', 't.flownder@gmail.com', '506-555-2351');
insert into Player values (134, 34, 'Sharon Marquez', 'Toronto', 'ON', 's.marquez@gmail.com', '249-555-5892');
insert into Player values (135, 34, 'Frankie White', 'Burnaby', 'BC', 'f.white420@yahoo.com', '236-842-2313');
insert into Player values (136, 34, 'Jason Williams', 'London', 'ON', 'jasonnn92@hotmail.com', '249-523-9562');
insert into Player values (137, 35, 'Kimberly Brown', 'Rosthey', 'NB', 'kimkim1978@live.ca', '506-678-2219');
insert into Player values (138, 35, 'Roy Waxer', 'Saint John', 'NB', 'waxer899@proton.me', '506-789-3482');
insert into Player values (139, 35, 'Jeff Kirkpatrick', 'St Stephen', 'NB', 'jjwilds4242@gmail.com', '506-642-2211');
insert into Player values (140, 35, 'Steve Buscemi', 'Cornwall', 'PE', 'itsmeSteve@stevesteve.com', '506-323-5421');
insert into Player values (141, 36, 'Jeff "The Dude" Lebowski', 'Los Angeles', 'US', 'thedude@aol.com', '213-843-3833');
insert into Player values (142, 36, 'Walter Sobchak', 'Los Angeles', 'US', 'nam4ever@aol.com', '213-486-6266');
insert into Player values (143, 36, 'Donny Kerabastos', 'Los Angeles', 'US', 'surf1ngD@aol.com', '213-366-7873');
insert into Player values (144, 36, 'Jeff Bridges', 'Los Angeles', 'US', 'jBridges@aol.com', '213-533-8766');
insert into Player values (145, 37, 'Homer Simpson', 'Springfield', 'US', 'smrtman@wang.com', '636-555-8707');
insert into Player values (146, 37, 'Moe Szyslak', 'Springfield', 'US', 'beermanmoe@wang.com', '800-555-0000');
insert into Player values (147, 37, 'Apu Nahasapeemapetilon', 'Springfield', 'US', 'apu@kwik-e-mart.com', '636-555-7823');
insert into Player values (148, 37, 'Dan Gillick', 'Springfield', 'US', 'accounts@springfield-mafia.com', '636-555-8997');
insert into Player values (149, 38, 'Justin Treadau Jr.', 'Ottawa', 'ON', 'justintreadau.02@canadagov.ca', '543-555-4567');
insert into Player values (150, 38, 'Mark Carney', 'Ottawa', 'ON', 'markcarney@canadagov.ca', '534-563-5355');
insert into Player values (151, 38, 'Justin Treadau Sr.', 'Ottawa', 'ON', 'justintreadau@canadagov.ca', '523-760-5765');
insert into Player values (152, 38, 'Stephen Harper', 'Ottawa', 'ON', 'stephenharper@canadagov.ca', '506-275-4648');
insert into Player values (153, 39, 'Erica Grutzner', 'Saint John', 'NB', 'ericag@gmail.com', '506-543-5634');
insert into Player values (154, 39, 'Sandra Wozniac', 'Moncton', 'NB', 'sanwoz@hotmail.com', '532-434-4871');
insert into Player values (155, 39, 'Gwen Miller', 'Sussex', 'NB', 'gwenmill123@gmail.com', '645-566-3555');
insert into Player values (156, 39, 'Barb Herman', 'Baxters Corner', 'NB', 'barbherman@yahoo.com', '987-432-4643');
insert into Player values (157, 40, 'Manny Manual', 'Kingston ', 'NB', 'manman@gmail.com', '426-953-8653');
insert into Player values (158, 40, 'Arin hanson', 'Victoria', 'BC', 'arinhanson33@gmail.com', '543-862-8343');
insert into Player values (159, 40, 'Jackmerius Tactheratrix', 'Toronto', 'ON', 'jacktac@hotmail.com', '872-345-9436');
insert into Player values (160, 40, 'Hingle McCringleberry', 'Quebec City', 'QC', 'hingcring@gmail.com', '434-775-3245');
insert into Player values (161, 41, 'Jordan Ellis', 'Detroit', 'MI', 'Jordan.elis@gmail.com', '313-442-8810');
insert into Player values (162, 41, 'Mia Thompson', 'Windsor', 'ON', 'mia.t@gmail.com', '519-734-9065');
insert into Player values (163, 41, 'Carlos Rivera', 'Cleveland', 'OH', 'carlos.r@gmail.com', '216-503-7708');
insert into Player values (164, 41, 'Emily Brooks', 'London', 'ON', 'emily.brooks@gmail.com', '226-439-1182');
insert into Player values (165, 42, 'Noah Jenkins', 'Chicago', 'IL', 'noah.jenkins@gmail.com', '312-672-5541');
insert into Player values (166, 42, 'Alyssa Green', 'Toronto', 'ON', 'alyssa.green@gmail.com', '416-893-2277');
insert into Player values (167, 42, 'Ethan Scott', 'Minneapolis', 'MN', 'ethan.scott@gmail.com', '612-498-9910');
insert into Player values (168, 42, 'Olivia Martin', 'Ottawa', 'ON', 'olivia.martin@gmail.com', '613-875-6423');
insert into Player values (169, 43, 'Mason Lee', 'Vancouver', 'BC', 'mason.lee@gmail.com', '604-391-2246');
insert into Player values (170, 43, 'Ava Patel', 'Seattle', 'WA', 'ava.patel@gmail.com', '206-782-4489');
insert into Player values (171, 43, 'Harper Davis', 'Edmonton', 'AB', 'harper.davis@gmail.com', '587-204-5132');
insert into Player values (172, 43, 'Liam Walker', 'Portland', 'OR', 'liam.walker@gmail.com', '503-910-4408');
insert into Player values (173, 44, 'Chloe Brown', 'Halifax', 'NS', 'chloe.brown@gmail.com', '902-775-9912');
insert into Player values (174, 44, 'Jacob Wilson', 'Boston', 'MA', 'jacob.wilson@gmail.com', '617-330-8945');
insert into Player values (175, 44, 'Emma Lopez', 'New York City', 'NY', 'emma.lopez@gmail.com', '212-694-0027');
insert into Player values (176, 44, 'Benjamin Carter', 'Calgary', 'AB', 'benjamin.carter@gmail.com', '403-521-6408');
insert into Player values (177, 45, 'Grace Parker', 'Miami', 'FL', 'grace.parker@gmail.com', '305-847-9082');
insert into Player values (178, 45, 'Dylan Nguyen', 'Montreal', 'QC', 'dylan.nguyen@gmail.com', '514-606-1129');
insert into Player values (179, 45, 'Sophia Hernandez', 'Dallas', 'TX', 'sophia.hernandez@gmail.com', '972-441-7715');
insert into Player values (180, 45, 'Lucas Johnson', 'Regina', 'SK', 'lucas.johnson@gmail.com', '306-739-6650');
insert into Player values (181, 46, 'Pedri Gonzalez', 'Barcelona', 'CT', 'pedri.gonzalez@fcb.com', '647-293-1825');
insert into Player values (182, 46, 'Lamine Yamal', 'Barcelona', 'CT', 'lamine.yamal@fcb.com', '438-671-9002');
insert into Player values (183, 46, 'Alejandro Balde', 'Barcelona', 'CT', 'alejandro.balde@fcb.com', '514-882-7496');
insert into Player values (184, 46, 'Fermin Lopez', 'Barcelona', 'CT', 'fermin.lopez@fcb.com', '289-350-4611');
insert into Player values (185, 47, 'Anthony Stewart The IVth', 'Amsterdam', 'ZE', 'anthony.iv.stewart@royalwave.com', '416-720-3928');
insert into Player values (186, 47, 'Josselyn Montpellieur', 'Bordeaux', 'NA', 'josselyn.monty@velvetmail.net', '437-554-0067');
insert into Player values (187, 47, 'Dresden Dusseldorf', 'Dortmund', 'NW', 'dresden.dd@nightbyte.io', '905-778-2394');
insert into Player values (188, 47, 'Dr. Remira Istambuli ', 'Ankara', 'AN', 'dr.remira.i@medinex.org', '204-613-5579');
insert into Player values (189, 48, 'Lionel Pessi', 'Rosario', 'SF', 'lionelpessi@goatmail.com', '613-972-4446');
insert into Player values (190, 48, 'Cristiano Penaldo', 'Funchal', 'MA', 'cpenaldo@kickzone.com', '705-663-1820');
insert into Player values (191, 48, 'Kylian Mbappenal', 'Paris', 'PA', 'kylian.mbapp@hypergoal.net', '782-290-5113');
insert into Player values (192, 48, 'Robert Lewangoalski', 'Warsaw', 'PO', 'robert.lg@strikemail.com', '902-714-2608');
insert into Player values (193, 49, 'Vivian Thompson', 'Indianapolis', 'IN', 'vivian.t@aurora-mail.com', '628-337-9010');
insert into Player values (194, 49, 'Anna Franks', 'Frankfurt', 'GE', 'anna.f@outlook.com', '310-840-9934');
insert into Player values (195, 49, 'Joan of Arc', 'Domremy-la-Pucelle', 'FR', 'joan.arc@flameborn.co', '312-670-5290');
insert into Player values (196, 49, 'Rosa Parks', 'Tuskegee', 'AL', 'rosa.p@couragehub.com', '415-907-2864');
insert into Player values (197, 50, 'Mark Zuckerberg', 'New York', 'NY', 'mark.z@metanet.ai', '236-419-8752');
insert into Player values (198, 50, 'Kevin Systrom', 'Hollinston', 'MA', 'kevin.s@pixlane.com', '778-302-6619');
insert into Player values (199, 50, 'Jack Patrick Dorsey', 'San Luis', 'MI', 'jack.pd@skycode.dev', '587-924-1035');
insert into Player values (200, 50, 'Evan Spiegel ', 'Los Angeles', 'CA', 'evan.s@ghostlabs.app', '431-667-2984');

COMMIT;

-- The End --
