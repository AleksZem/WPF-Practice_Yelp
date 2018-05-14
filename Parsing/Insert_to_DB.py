import json
import psycopg2
import string

def business_insertstatements():
	with open("yelp_business.txt",'r', encoding="utf8") as business:
		outfile = open(".//yelp_business_inserts.txt",'w', encoding="utf8")
		try:
			conn = psycopg2.connect("dbname='postgres' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()
		print("Business Yelp Inserts")
		line = business.readline().rstrip("\n").split('\t')
		count = 0
		while line:
			if len(line) != 12:
				break
			sql_str = "INSERT INTO yelp_businesses (bid, bname, baddress,bstate,bcity,blat,blong,bavgstars,brevcount,bopen,bcats,numCheckins,bhours) \n" \
                      "VALUES ('" + line[0] + "','" + line[1] + "','" + line[2] + "','" + \
                       line[3] + "','" + line[4] + "'," + line[5] + "," + line[6] + "," + \
                       line[7] + "," + line[8] + "," + line[9] + ",'" + line[10] +"'" + ",0 ,'" + \
                       line[11]+ "'); \n"
			try:
				cur.execute(sql_str)
				count+=1
				print("Inserted Business #",count)
			except:
				print("Insert to businessTABLE failed!")
				print(sql_str)
			conn.commit()
			try:
				line=business.readline().rstrip("\n").split('\t')
			except:
				print("UnicodeEncodeError, if last entry it's fine")
			outfile.write(sql_str)
	cur.close()
	conn.close()
	business.close()

def user_insertstatements():
	with open("yelp_user.txt",'r',encoding="utf8") as user:
		outfile = open(".//yelp_user_inserts.txt",'w',encoding="utf8")
		try:
			conn = psycopg2.connect("dbname='postgres' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()
		print("User Yelp Inserts")
		line = user.readline().rstrip("\n").split('\t')
		count = 0
		while line:
			if len(line)!=11:
				break
			sql_str = "INSERT INTO yelp_users (uid,uname,type,ufriends,uavgstar,urevcount,uyelpsince,ufans,uvotes,uelite,ucompliments)\n" \
				"VALUES ('" + line[0] + "','" + line[1] + "','" + line[2] + "','" + \
				line[3] + "'," + line[4]+ "," + line[5] + ",'" + line[6]+ "'," + line[7] + ",'" + \
				line[8] + "','" + line[9] + "','" + line[10] + "'); \n"
			try:
				cur.execute(sql_str)
				count+=1
				print("Inserted User #",count)
			except:
				print("Insert to userTABLE failed!",sql_str,"\n")
			conn.commit()
			#print("\n1: ",sql_str,"\n")
			line = user.readline().rstrip("\n").split('\t')
			#print("\n2: LINE:",line,"\n")
			outfile.write(sql_str)
	cur.close()
	conn.close()
	user.close()	
	
def checkin_insertstatements():
	with open("yelp_checkin.txt","r",encoding="utf8") as checkin:
		outfile=open(".//yelp_checkin_inserts.txt", 'w', encoding="utf8")
		try:
			conn=psycopg2.connect("dbname='postgres' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()
		print("Check Ins Yelp Inserts")
		line=checkin.readline().rstrip("\n").split('\t')
		count = 0
		while line:
			if len(line) != 8:
				break
			sql_str = "INSERT INTO yelp_checkins\n" \
			"VALUES ('"+line[0]+"','"+line[1]+"','" +line[2]+ "','" + line[3] + "','" + \
			line[4] + "','" + line[5] + "','" + line[6] + "','" + line[7] + "')"
			try:
				cur.execute(sql_str)
				count+=1
				print("Inserted Checkin #",count)
			except:
				print("Insert to checkinsTABLE failed!")
				print(sql_str)
			conn.commit()
			try:
				line=checkin.readline().rstrip("\n").split('\t')
			except:
				print("Expected error")
			outfile.write(sql_str)
	cur.close()
	conn.close()
	checkin.close()

def tip_insertstatements():
	with open("yelp_tip.txt",'r',encoding="utf8") as tip:
		outfile = open(".//yelp_tip_inserts.txt",'w',encoding="utf8")
		try:
			conn = psycopg2.connect("dbname='postgres' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()
		print("Tip Yelp Inserts")
		line = tip.readline().rstrip("\n").split('\t')
		print(line,len(line))
		count = 0
		while line:
			if len(line)!=6:
				break
			sql_str = "INSERT INTO yelp_tips (uid,tip_text,bid,tip_date,type,tip_likes,Tip_id) \n" \
					"VALUES ('" +line[0] + "','" + line[1]+"','" + line[2] + "','" + line[3] + "','" \
					+ line[4] + "'," + line[5] + ",DEFAULT);\n"
			try:
				cur.execute(sql_str)
				count+=1
				print("Inserted Tip #",count)
			except:
				print("Insert to tipTABLE failed!: ",sql_str)
			conn.commit()
			line = tip.readline()
			line = ''.join(x for x in line if x in string.printable)
			line = line.rstrip("\n").split('\t')
			outfile.write(sql_str)
	cur.close()
	conn.close()
	tip.close()
	
	
business_insertstatements()
user_insertstatements()
checkin_insertstatements()
tip_insertstatements()
#x = input()