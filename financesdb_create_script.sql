create database financesdb;

use financesdb;

create table users(
	id_user int auto_increment primary key,
    name_user varchar(100) not null unique,
    password_user varchar(50) not null
);

create table categories(
	id_category int auto_increment primary key,
    name_category varchar(100) not null,
    id_user_category int not null,
	FOREIGN KEY (id_user_category) references users(id_user)
);

create table establishments(
	id_establishment int auto_increment primary key,
    name_establishment varchar(100) not null,
    id_user_establishment int not null,
    FOREIGN KEY (id_user_establishment) REFERENCES users(id_user)
);

create table expenses(
	id_expense int auto_increment primary key,
    date_expense date not null,
    value_expense decimal(10,2) not null,
    id_establishment_expense int not null,
    id_category_expense int not null,
    id_user_expense int not null,
	FOREIGN KEY (id_user_expense) references users(id_user),
    FOREIGN KEY(id_establishment_expense) REFERENCES establishments(id_establishment),
    FOREIGN KEY (id_category_expense) REFERENCES categories(id_category)
);
