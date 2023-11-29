create database EPE3;
use EPE3;

CREATE TABLE Medico(
    idMedico INT AUTO_INCREMENT,
    NombreMed VARCHAR(50),
    ApellidoMed VARCHAR(50),
    RunMed VARCHAR(50),
    Eunacom VARCHAR(5),
    NacionalidadMed VARCHAR(45),
    Especialidad VARCHAR(45),
    Horarios TIME,
    TarifaHr INT,
    PRIMARY KEY(idMedico),
    INDEX(NombreMed, ApellidoMed, RunMed, Eunacom, NacionalidadMed, Especialidad, Horarios, TarifaHr)
);

CREATE TABLE Paciente (
    idPaciente INT AUTO_INCREMENT,
    NombrePac VARCHAR(30), -- Reducir la longitud
    ApellidoPac VARCHAR(30), -- Reducir la longitud
    RunPac VARCHAR(25),
    Nacionalidad VARCHAR(50),
    Visa VARCHAR(5),
    Genero VARCHAR(10),
    SintomasPac VARCHAR(50), -- Reducir la longitud
    Medico_idMedico INT,
    PRIMARY KEY(idPaciente),
    INDEX(NombrePac, ApellidoPac, RunPac, Nacionalidad, Visa, Genero, SintomasPac, Medico_idMedico),
    FOREIGN KEY(Medico_idMedico) REFERENCES Medico(idMedico)
);


CREATE TABLE Reserva(
    idReserva INT AUTO_INCREMENT,
    Especialidad VARCHAR(45),
    DiaReserva DATE,
    Paciente_idPaciente INT,
    PRIMARY KEY(idReserva),
    INDEX(Especialidad, DiaReserva, Paciente_idPaciente),
    FOREIGN KEY(Paciente_idPaciente) REFERENCES Paciente(idPaciente)
);

