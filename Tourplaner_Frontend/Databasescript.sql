Drop Table Tour CASCADE;
Drop Table Tour_Log CASCADE;


CREATE TABLE Tour (
    TID serial PRIMARY KEY ,
    Name varchar(255),
    StartLocation double precision,
    FinishLocation double precision
);

CREATE TABLE Tour_Log (
    TLID serial PRIMARY KEY,
    TID int,
    DateTime timestamp,
    Report varchar(255),
    Distance int,
    Rating int
    CONSTRAINT TID references Tour(TID)
);


CREATE OR REPLACE FUNCTION insert_tourlog(i_TID int, TS_Moment timestamp, vchar_rep varchar(255), i_dist int,i_rating int) RETURNS int AS $$
DECLARE
    i_tcounter int;
BEGIN
    select count(*) into  i_tcounter from Tour where TID == i_TID;
    IF i_tcounter == 0 THEN
        return -1;
    END IF;
    Insert Into Tour_Log( TID, DateTime,Report,Distance,Rating) VALUES (i_TID,TS_Moment,vchar_rep,i_dist,i_rating);

END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION insert_tour(s_Name varchar(255), StartLoc double precision, Finish double precision) RETURNS int AS $$
DECLARE
BEGIN

    Insert Into Tour( Name,StartLocation,FinishLocation) VALUES (s_Name,StartLoc,Finish);

END;
$$ LANGUAGE plpgsql;

SELECT * from Tour;