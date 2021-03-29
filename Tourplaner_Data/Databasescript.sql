Drop Table Tour CASCADE;
Drop Table Tour_Log CASCADE;


CREATE TABLE Tour
(
    TID    serial PRIMARY KEY,
    Name   varchar(255) unique,
    Description varchar(255),
    Source varchar(255),
    Destination   varchar(255),
    Distance int
);

CREATE TABLE Tour_Log
(
    TLID     serial PRIMARY KEY,
    TID      int
        CONSTRAINT TID_Tour references Tour (TID),
    DateTime timestamp,
    Report   varchar(255),
    Rating   int,
    Deaths int,
    Difficulty int,
    Energy_burn int,
    Water_rec float,
    try_dist float,
    try_time int,
    Average_speed float,
    Health_Rating int
);


CREATE OR REPLACE FUNCTION insert_tourlog(i_TID int, TS_Moment timestamp, vchar_rep varchar(255),
                                          i_rating int, i_deaths int, i_Dificulty int, i_Engergyburn int,
                                          f_try_dist float,
                                          i_try_time int,
                                          f_Water_rec float,
                                          f_average_speed float,
                                          i_Health_Rating int) RETURNS int AS
$$
DECLARE
    i_tcounter int;
BEGIN
    select count(*) into i_tcounter from Tour where TID = i_TID;
    IF i_tcounter = 0 THEN
        return -1;
    END IF;
    Insert Into Tour_Log(TID, DateTime, Report, Rating, Deaths, Difficulty,Energy_burn,Water_rec,try_dist, try_time, Average_speed, Health_Rating)
    VALUES (i_TID, TS_Moment, vchar_rep, i_rating, i_deaths, i_Dificulty, i_Engergyburn,f_Water_rec,f_try_dist, i_try_time, f_try_dist/i_try_time, i_Health_Rating);

END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION insert_tours(s_Name varchar(255), s_desc varchar(255),s_source varchar(255), s_dest varchar(255), i_Dist int) RETURNS int AS
$$
DECLARE
    i_tcounter int;
BEGIN
    select count(*) into i_tcounter from Tour where Name Like s_Name;
    IF i_tcounter > 0 THEN
        return -1;
    END IF;
    Insert Into Tour(Name,Description, source, destination, Distance) VALUES (s_Name,s_desc, s_source, s_dest, i_Dist);
    return 0;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION copy_tour(s_Name varchar(255)) RETURNS int AS
$$
DECLARE
    i_tcounter int;
BEGIN
    select count(*) into i_tcounter from Tour where Name = s_Name;
    IF i_tcounter = 0 THEN
        return -1;
    ELSE
        IF i_tcounter > 2 THEN
            return -2;
        end if;
    END IF;
    select TID into i_tcounter from Tour where Name = s_Name;
    Insert Into Tour(Name,Description, source, destination, Distance) ((Select concat(NAme, '_copy'), Description,source, destination,Distance
                                           from Tour
                                           Where TID = i_tcounter));
    return 0;
END;
$$ LANGUAGE plpgsql;

SELECT insert_tours('Kurze Runde','Short Walk', 'Illmitz', 'Apetlon', 2);
SELECT copy_tour('Kurze Runde_copy');

select *
from Tour;

Select *
from Tour
WHERE Name Like '%%';

Delete
FROM Tour
Where Name = 'Kurze Runde_copy_copy';
COMMIT;