Drop Table Tour CASCADE;
Drop Table Tour_Log CASCADE;


CREATE TABLE Tour
(
    TID    serial PRIMARY KEY,
    Name   varchar(255) unique,
    source varchar(255),
    destination   varchar(255)
);

CREATE TABLE Tour_Log
(
    TLID     serial PRIMARY KEY,
    TID      int
        CONSTRAINT TID_Tour references Tour (TID),
    DateTime timestamp,
    Report   varchar(255),
    Distance int,
    Rating   int

);


CREATE OR REPLACE FUNCTION insert_tourlog(i_TID int, TS_Moment timestamp, vchar_rep varchar(255), i_dist int,
                                          i_rating int) RETURNS int AS
$$
DECLARE
    i_tcounter int;
BEGIN
    select count(*) into i_tcounter from Tour where TID = i_TID;
    IF i_tcounter = 0 THEN
        return -1;
    END IF;
    Insert Into Tour_Log(TID, DateTime, Report, Distance, Rating)
    VALUES (i_TID, TS_Moment, vchar_rep, i_dist, i_rating);

END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION insert_tours(s_Name varchar(255), s_source varchar(255), s_dest varchar(255)) RETURNS int AS
$$
DECLARE
    i_tcounter int;
BEGIN
    select count(*) into i_tcounter from Tour where Name Like s_Name;
    IF i_tcounter > 0 THEN
        return -1;
    END IF;
    Insert Into Tour(Name, source, destination) VALUES (s_Name, s_source, s_dest);
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
    Insert Into Tour(Name, source, destination) ((Select concat(NAme, '_copy'), source, destination
                                           from Tour
                                           Where TID = i_tcounter));
    return 0;
END;
$$ LANGUAGE plpgsql;

SELECT insert_tours('Kurze Runde', 'Illmitz', 'Apetlon');
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