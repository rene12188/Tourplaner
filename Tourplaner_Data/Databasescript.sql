Drop Table Tour CASCADE;
Drop Table Tour_Log CASCADE;


CREATE TABLE Tour
(
    TID    serial PRIMARY KEY,
    Name   varchar(255) unique,
    Description varchar(255),
    Source varchar(255),
    Destination   varchar(255),
    Distance double precision /*in KM*/
);

CREATE TABLE Tour_Log
(
    TLID     serial PRIMARY KEY,
    TID      int
        CONSTRAINT TID_Tour references Tour (TID),
    DateTime timestamp,
    Report   varchar(255),
    Distance double precision, /*in KM*/
    Totaltime int, /*in Minutes*/
    Rating   int, /*1/Easy-5/Hard*/

    AvgSpeed double precision,
    Difficulty int, /*1/Easy-5/Hard*/
    EnergyBurn int,
    Temperature int,
    WaterRecomendation double precision /*in ml*/
);


CREATE OR REPLACE FUNCTION insert_tourlog(i_TID int,
                                          s_report varchar,
                                          ts_Moment timestamp,
                                          i_Distance double precision,
                                          i_Totaltime int,
                                          i_Rating int,
                                          i_Difficulty int,
                                          i_Temperature int
                                          ) RETURNS int AS
$$
DECLARE
    i_tcounter int;
    i_distancecheck double precision;
    i_calories int;
BEGIN
    select count(*) into i_tcounter from Tour where TID = i_TID AND Distance > i_Distance;
    IF i_tcounter = 0 THEN
        return -1;
    END IF;

    i_calories = 6.66*i_Distance*i_Difficulty;
    Insert Into Tour_Log(TID, DateTime, Report, Distance, Totaltime,Rating,AvgSpeed , Difficulty,EnergyBurn,Temperature,WaterRecomendation)
    VALUES (i_TID, TS_Moment, s_report, i_rating, i_Totaltime, i_Rating, i_Distance/i_Totaltime,i_Difficulty,i_calories, i_Temperature, (i_Distance*100)+20);
    return 0 ;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION insert_tours(s_Name varchar(255), s_desc varchar(255),s_source varchar(255), s_dest varchar(255), i_Dist double precision) RETURNS int AS
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

CREATE OR REPLACE FUNCTION Nuke() RETURNS int AS
$$
DECLARE
BEGIN
Delete FROM Tour_Log;
Delete FROM Tour;
    return 0;
END;
$$ LANGUAGE plpgsql;

SELECT insert_tours('Kurze Runde','Short Walk', 'Illmitz', 'Apetlon', 2);
SELECT copy_tour('Kurze Runde_copy');

Select insert_tourlog(2, 'THis is a report' , current_date, 1.0,60,3,3,20);


select *
from Tour;

Select *
from Tour
WHERE Name Like '%%';

Select DateTime,Report,Distance,Totaltime,Rating,AvgSpeed,Difficulty,EnergyBurn,Temperature,WaterRecomendation
from Tour_Log;


Delete
FROM Tour
Where Name = 'Weite Runde';
COMMIT;