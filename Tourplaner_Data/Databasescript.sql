Drop Table Tour CASCADE;
Drop Table Tour_Log CASCADE;


CREATE TABLE Tour (
    TID serial PRIMARY KEY ,
    Name varchar(255) unique,
    Start_long double precision,
    Start_lat  double precision,
    Finish_long double precision,
    Finish_lat double precision
);

CREATE TABLE Tour_Log (
    TLID serial PRIMARY KEY,
    TID int CONSTRAINT TID_Tour references Tour(TID),
    DateTime timestamp,
    Report varchar(255),
    Distance int,
    Rating int

);


CREATE OR REPLACE FUNCTION insert_tourlog(i_TID int, TS_Moment timestamp, vchar_rep varchar(255), i_dist int,i_rating int) RETURNS int AS $$
DECLARE
    i_tcounter int;
BEGIN
    select count(*) into  i_tcounter from Tour where TID == i_TID;
    IF i_tcounter = 0 THEN
        return -1;
    END IF;
    Insert Into Tour_Log( TID, DateTime,Report,Distance,Rating) VALUES (i_TID,TS_Moment,vchar_rep,i_dist,i_rating);

END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION insert_tours(s_Name varchar(255), D_Start_lat  double precision ,D_Start_long double precision ,D_Finish_lat  double precision,D_Finish_long double precision) RETURNS int AS $$
DECLARE
BEGIN

    Insert Into Tour( Name,Start_lat, Start_long  ,Finish_lat,Finish_long  ) VALUES (s_Name ,D_Start_lat , D_Start_long,D_Finish_lat  ,D_Finish_long );
    return 0;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION copy_tour(s_Name varchar(255)) RETURNS int AS $$
DECLARE
    i_tcounter int;
BEGIN
  select count(*) into  i_tcounter from Tour where Name = s_Name;
    IF i_tcounter = 0 THEN
        return -1;
    ELSE IF i_tcounter > 2 THEN
        return -2;
    end if;
    END IF;
    select TID into  i_tcounter from Tour where Name = s_Name;
    Insert Into Tour( Name,Start_lat, Start_long  ,Finish_lat,Finish_long  )  ((Select concat(NAme, '_copy'), Start_lat , Start_long,Finish_lat  ,Finish_long from Tour Where TID = i_tcounter));
    return 0;
END;
$$ LANGUAGE plpgsql;

SELECT insert_tours('Kurze Runde', 47.7614800, 16.8002400,47.74429, 16.8301373);
SELECT copy_tour('Kurze Runde_copy');
select *  from Tour;
Select concat(NAme, '_copy'), Start_lat , Start_long,Finish_lat  ,Finish_long from Tour Where TID = 1;

Select * from Tour WHERE Name Like '%%';

Delete FROM Tour Where Name = 'Kurze Runde_copy_copy';
COMMIT;