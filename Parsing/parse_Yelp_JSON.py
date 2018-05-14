import json
#import psycopg2
#import codecs
import sys

def cleanStr4SQL(s):
    return s.replace("'","''").replace("\n"," ").replace("\\","")

def parseBusinessData():
    #read the JSON file
    with open('.//yelp_business.JSON','r',encoding='utf8') as f:  #TO DO: update the path to the JSON file
        outfile =  open('.//yelp_business.txt', 'w',encoding='utf8') #TO DO: update the path to the output file
        print ('Parsing yelp_business.JSON :')
        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+ '\t') #business id
            outfile.write(cleanStr4SQL(data['name'])+ '\t') #name
            outfile.write(cleanStr4SQL(data['full_address'])+ '\t') #full_address
            outfile.write(cleanStr4SQL(data['state']) + '\t') #state
            outfile.write(cleanStr4SQL(data['city'])+'\t') #city
            outfile.write(str(data['latitude'])+'\t') #latitude
            outfile.write(str(data['longitude'])+'\t') #longitude
            outfile.write(str(data['stars'])+'\t') #stars
            outfile.write(str(data['review_count'])+'\t') #reviewcount
            outfile.write(str(data['open'])+'\t') #openstatus
            outfile.write('{')
            for index in range(0, len(data['categories']) - 1):
                outfile.write(data['categories'][index]+ ',')
            if(len(data['categories'])>=1):
                outfile.write(data['categories'][-1])
            outfile.write('}' + '\t')
                
                
            hourslist = list((map(list,data['hours'].items())))
            outfile.write('{')
            for index in range(0, len(hourslist)-1):
                outfile.write('{' +hourslist[index][0]+','
                              +hourslist[index][1]['open']+','
                              +hourslist[index][1]['close']+ '}' +','
                )
            if(len(hourslist)>=1):
                outfile.write('{' +hourslist[-1][0]+','
                              +hourslist[-1][1]['open']+','
                              +hourslist[-1][1]['close']+ '}'
                    )
            outfile.write('}')
            outfile.write("\n")
            line = f.readline()
            count_line +=1
            if (count_line % 2000) == 0:
                print('.',end='')
                sys.stdout.flush()
    outfile.close()
    f.close()
    print ('\nDONE (total ',count_line, ' lines parsed')



def parseTipsData():
    #reading the JSON file
    with open('.//yelp_tip.JSON','r',encoding='utf8') as f: #TO DO: update the path to the JSON file
        outfile =  open('.//yelp_tip.txt', 'w',encoding='utf8') #TO DO: update the path to the output file
        print ('Parsing yelp_tip.JSON :')
        line = f.readline()
        count_line = 0
        while line:
            #TO DO: parse the tip data
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['user_id'])+'\t')
            outfile.write(cleanStr4SQL(data['text'])+'\t')
            outfile.write(cleanStr4SQL(data['business_id'])+'\t')
            outfile.write(cleanStr4SQL(data['date'])+'\t')
            outfile.write(cleanStr4SQL(data['type'])+'\t')
            outfile.write(str(data['likes']))
            outfile.write("\n")
            line = f.readline()
            count_line +=1
            if (count_line % 2000) == 0:
                print('.',end='')
                sys.stdout.flush()

    outfile.close()
    f.close()
    print ('\nDONE (total ',count_line, ' lines parsed')


def parseUserData():
    #reading the JSON file
    with open('.//yelp_user.JSON','r',encoding='utf8') as f:
        outfile =  open('.//yelp_user.txt', 'w',encoding='utf8')
        print ('Parsing yelp_user.JSON :')
        line = f.readline()
        count_line = 0
        while line:
            data = json.loads(line)
            #TO DO: parse the user data
            outfile.write(cleanStr4SQL(data['user_id'])+'\t')
            outfile.write(cleanStr4SQL(data['name'])+'\t')
            outfile.write(cleanStr4SQL(data['type'])+'\t')
            outfile.write("{")
            for index in range(0,len(data['friends'])-1):
                outfile.write(data['friends'][index] + ",")
            if (len(data['friends']) >= 1):
                outfile.write(data['friends'][-1])
            outfile.write("}"+'\t')
            outfile.write(str(data['average_stars'])+'\t')
            outfile.write(str(data['review_count'])+'\t')
            outfile.write(str(data['yelping_since'])+'\t')
            outfile.write(str(data['fans'])+'\t')
            votelist = list((map(list,data['votes'].items())))
            outfile.write("{")
            for index in range(0,len(votelist)-1):
                outfile.write('{' + str(votelist[index][0]) + ','
                                + str(votelist[index][1]) + '}' +',')
            if (len(votelist)>=1):
                outfile.write('{' + str(votelist[-1][0]) + ','
                                + str(votelist[-1][1]) + '}')
            outfile.write("}"+'\t')
            outfile.write("{")
            for index in range(0,len(data['elite'])-1):
                outfile.write(str(data['elite'][index])+",")
            if (len(data['elite']) >= 1):
                outfile.write(str(data['elite'][-1]))
            outfile.write("}"+'\t')
            
            complimentslist = list((map(list,data['compliments'].items())))
            outfile.write("{")
            for index in range(0,len(complimentslist)-1):
                outfile.write('{' + str(complimentslist[index][0]) + ','
                                + str(complimentslist[index][1]) + '}' +',')
            if (len(complimentslist)>=1):
                outfile.write('{' + str(complimentslist[-1][0]) + ','
                                + str(complimentslist[-1][1]) + '}')
            outfile.write("}")
            outfile.write("\n")
            line = f.readline()
            count_line +=1
            if (count_line % 2000) == 0:
                print('.',end='')
                sys.stdout.flush()
    outfile.close()
    f.close()
    print ('\nDONE (total ',count_line, ' lines parsed')




def addupCheckins (raw_checkins):
    checkins = {}
    for date_time_str,num in raw_checkins.items():
        date_time = date_time_str.split('-')
        date_time[0] = int(date_time[0])
        date_time[1] = int(date_time[1])
        #update the totals in the checkins dictionary accordingly.
        if date_time[0]>=6 and date_time[0]<12:
            temp_dict  = checkins.get(date_time[1],{})
            temp_dict["morning"] = temp_dict.get("morning",0) + num
            checkins[date_time[1]] = temp_dict
        elif date_time[0]>=12 and date_time[0]<17:
            temp_dict  = checkins.get(date_time[1],{})
            temp_dict["afternoon"] = temp_dict.get("afternoon",0) + num
            checkins[date_time[1]] = temp_dict
        elif date_time[0]>=17 and date_time[0]<23:
            temp_dict  = checkins.get(date_time[1],{})
            temp_dict["evening"] = temp_dict.get("evening",0) + num
            checkins[date_time[1]] = temp_dict
        elif date_time[0]>=23 or date_time[0]<6:
            temp_dict  = checkins.get(date_time[1],{})
            temp_dict["night"] = temp_dict.get("night",0) + num
            checkins[date_time[1]] = temp_dict
    return checkins



def parseCheckinData():
    #reading the JSON file
    with open('.//yelp_checkin.JSON','r',encoding='utf8') as f:
        outfile =  open('.//yelp_checkin.txt', 'w',encoding='utf8')
        print ('Parsing yelp_checkin.JSON :')
        line = f.readline()
        count_line = 0
        failed_inserts = 0
        while line:
            data = json.loads(line)
            checkin_dict = addupCheckins(data["checkin_info"])
            out_str = cleanStr4SQL(data['business_id'])
            weekcheckins = ['{0,0,0,0}']*7
            for dayofweek,checkin in checkin_dict.items():
                weekcheckins[dayofweek] =( "{" + str(checkin.get("morning",0))
                              + "," + str(checkin.get("afternoon",0)) + "," + str(checkin.get("evening",0)) + ","
                              + str(checkin.get("night",0)) +"}" )
            for item in weekcheckins: out_str = out_str + "\t" + str(item)
            outfile.write(out_str+"\n")
            line = f.readline()
            count_line +=1
            if (count_line % 2000) == 0:
                print('.',end='')
                sys.stdout.flush()

    outfile.close()
    f.close()
    print ('\nDONE (total ',count_line, ' lines parsed')



parseBusinessData()
parseUserData()
parseCheckinData()
parseTipsData()

