/*
**	This program is a template for SP lab 3 task 5 you are 
**	required to implement its one function.
*/
#define GREP 1
#define REPLACE 2

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

/*	
**	This function take file pointer as paramter to read content from and 
**	char pointer as an second argument which points to string to search in file
*/
void mygrep(FILE*, char*);

/*	
**	This function take file pointer as paramter to read content from and 
**	char pointer as an second argument which points to string to search in file
** 	and char pointer as an third argument to replace the string finded with it.
*/
void myreplace(FILE *fp,char *find, char * replace);

char *filename;
int  main(int argc,char *argv[])
{


	/*	creating variables	
*/
	char *exe=argv[0];
	int behaviour;
	FILE *fp;
	filename=argv[1];
	char *find=argv[2];
	char * replace;

	/*	check if mygrep is called or myreplace	
*/
	if(strcmp(exe,"./mygrep")==0 /*	check if the name of executable is mygrep	*/)
	{
		if(argc != 3)
		{
			printf("usage\t./mygrep filename <string-to-search>\n");
			exit(1);
		}

		behaviour = GREP;
	}
	else if(strcmp(exe,"./myreplace")==0 /*	check if the name of executable is myreplace	*/ )
	{
		if(argc != 4)
		{
			printf("\t./myreplace filename  <string-to-search> <string-to-replace>");
			exit(1);
		}
		behaviour = REPLACE;
		replace = argv[3];
	}
	else
	{
		behaviour = -1;
	}



	/* opening file	
*/

	fp=fopen(filename,"rtw");

	if(behaviour == GREP)
	{
		mygrep(fp,find);		/*	caling function	
*/
	}
	else if ( behaviour == REPLACE )
	{
		myreplace(fp,find,replace);		/*	calling myreplace	
*/
	}
	
	fclose(fp);		/*	closing file	
*/
	return 0;
}


void mygrep(FILE *fp,char *find)
{
	char c1[500];

	int i,count=0,occ=0;
	while(fgets(c1,500,fp)!=NULL)/*	read a string from file*/
	{
		/*	Add your code here to search a string find on string c1 readed from file	*/
		
   		 if(strstr(c1,find)!=NULL)
    		{
			 count++;
 	      		 printf("%d. %s\n",count,c1);
    
    		}

	}

}


void myreplace(FILE *fp,char *find, char * replace)
{
	char c1[500];
	int flen = strlen(find);
	FILE *fp1;
	char *newfile="mynewfile.txt";
	fp1=fopen(newfile,"wa");
	while(fgets(c1,100,fp)!=NULL && *c1 !='\n')
	{
		char *oneline=NULL;
	   	char* oneword;
		oneword = strtok(c1, " ");
		int first=0;
		while(oneword !=NULL)
		{	
			if(*oneword==*find && first==0)
			{
				printf("%s ", replace);
				fputs(replace,fp1);
				fputs(" ",fp1);						
				first=1;
			}
			else
			{
				printf("%s ", oneword);
				fputs(oneword,fp1);
				fputs(" ",fp1);
			}
			oneword = strtok(NULL, " ");
		}	
	}

	remove(filename);
	rename(newfile,filename);
}


