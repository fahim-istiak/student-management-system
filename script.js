function StudentViewModel() {
    const self = this;
    
    // Default avatar as a data URL (a simple gray circle)
    const DEFAULT_AVATAR = 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCAyNCAyNCI+PHBhdGggZmlsbD0iI2NjY2NjYyIgZD0iTTEyIDJDNi40OCAyIDIgNi40OCAyIDEyczQuNDggMTAgMTAgMTAgMTAtNC40OCAxMC0xMFMxNy41MiAyIDEyIDJ6bTAgM2MyLjY3IDAgNC44NCAyLjE3IDQuODQgNC44NCAwIDIuNjctMi4xNyA0Ljg0LTQuODQgNC44NC0yLjY3IDAtNC44NC0yLjE3LTQuODQtNC44NCAwLTIuNjcgMi4xNy00Ljg0IDQuODQtNC44NHptMCAxMmM0LjQyIDAgOC4xNy0yLjI4IDkuNTQtNS41MkgyLjQ2YzEuMzcgMy4yNCA1LjEyIDUuNTIgOS41NCA1LjUyeiIvPjwvc3ZnPg==';

    self.students = ko.observableArray([]);
    self.searchQuery = ko.observable('');
    self.isEditMode = ko.observable(false);
    self.newStudent = {
        firstName: ko.observable(''),
        lastName: ko.observable(''),
        age: ko.observable(''),
        photoUrl: ko.observable(DEFAULT_AVATAR)
    };

    self.filteredStudents = ko.computed(function() {
        const query = self.searchQuery().toLowerCase();
        if (!query) return self.students();
        
        return self.students().filter(function(student) {
            return student.firstName.toLowerCase().includes(query) || 
                   student.lastName.toLowerCase().includes(query);
        });
    });

    self.loadStudents = function() {
        const savedStudents = JSON.parse(localStorage.getItem('students') || '[]');
        self.students(savedStudents);
    };

    self.saveStudent = function() {
        if (!self.newStudent.firstName() || !self.newStudent.lastName() || !self.newStudent.age()) {
            alert('Please fill in all required fields');
            return;
        }

        const student = {
            firstName: self.newStudent.firstName(),
            lastName: self.newStudent.lastName(),
            age: self.newStudent.age(),
            photoUrl: self.newStudent.photoUrl()
        };

        self.students.push(student);

        localStorage.setItem('students', JSON.stringify(self.students()));

        self.newStudent.firstName('');
        self.newStudent.lastName('');
        self.newStudent.age('');
        self.newStudent.photoUrl(DEFAULT_AVATAR);
    };

    self.deleteStudent = function(student) {
            self.students.remove(student);
            localStorage.setItem('students', JSON.stringify(self.students()));
    };

    self.editStudent = function(student) {
        self.isEditMode(true);
        self.newStudent.firstName(student.firstName);
        self.newStudent.lastName(student.lastName);
        self.newStudent.age(student.age);
        self.newStudent.photoUrl(student.photoUrl);
        self.students.remove(student);
    };

    self.handlePhotoUpload = function(event) {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function(e) {
                self.newStudent.photoUrl(e.target.result); // base64 string
            };
            reader.readAsDataURL(file); // Converts to base64
        }
    };
    

    self.resetForm = function() {
        self.isEditMode(false);
        self.newStudent.firstName('');
        self.newStudent.lastName('');
        self.newStudent.age('');
        self.newStudent.photoUrl(DEFAULT_AVATAR);
    };
    self.loadStudents();
}

$(document).ready(function() {
    ko.applyBindings(new StudentViewModel());
}); 