pipeline {
    agent any
    
    stages {
        stage('Checkout') {
            steps {
                echo 'Checking out the code...'
                git 'https://github.com/Tiagooo90/BoxingGearReviewAPI.git'
            }
        }
        
        stage('Restore Dependencies') {
            steps {
                echo 'Restoring dependencies...'
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }
        
        stage('Run Tests') {
            steps {
                bat 'dotnet test'
            }
        }
        
        stage('Publish') {
            steps {
                bat 'dotnet publish --configuration Release --output ./publish'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploy Step - Configura o deploy para o teu servidor!'
            }
        }
    }

    environment {
        DOTNET_ROOT = 'C:\\Program Files\\dotnet' // Ajuste o caminho se necessário
    }
}

